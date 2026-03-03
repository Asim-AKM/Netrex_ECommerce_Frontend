using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Commons.Mappers.Products;
using Netrex.Frontend.Application.Services.ProductManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.ProductManagement;
using Netrex.Frontend.Blazor.Services;
using System.Net.Http.Json;

namespace Netrex.Frontend.Application.Services.ProductManagement.Implementations
{
    public class ProductManager : IProductManager
    {
        private readonly HttpClient _httpClient;
        private readonly LoaderService _loaderService;
        private readonly ICloudnaryManager _cloudnaryManager;

        public ProductManager(IHttpClientFactory httpClientFactory,
                              LoaderService loaderService,
                              ICloudnaryManager cloudnaryManager)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _loaderService = loaderService;
            _cloudnaryManager = cloudnaryManager;
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                ".bmp" => "image/bmp",
                ".tiff" or ".tif" => "image/tiff",
                _ => "image/jpeg"
            };
        }

        public async Task<ApiResponse<ProductsVm>> AddProducts(
            ProductsVm productsVm,
            List<byte[]> imageBytes,
            List<string> imageNames)
        {
            try
            {
                _loaderService.Show();

                if (imageBytes == null || imageBytes.Count == 0)
                    return ApiResponseDeserializer.FailResponse<ProductsVm>("No images to upload");

                string contentType = GetContentType(imageNames[0]);
                List<CloudinaryUploadResult> uploadedImages;

                if (imageBytes.Count == 1)
                {
                    var uploadResponse = await _cloudnaryManager.UploadToCloudinaryAsync<CloudinaryUploadResult>(
                        imageBytes, imageNames, contentType);
                    if (!uploadResponse.IsSuccess || uploadResponse.Data == null)
                        return ApiResponseDeserializer.FailResponse<ProductsVm>($"Upload failed: {uploadResponse.Message}");
                    uploadedImages = new List<CloudinaryUploadResult> { uploadResponse.Data };
                }
                else
                {
                    var uploadResponse = await _cloudnaryManager.UploadToCloudinaryAsync<List<CloudinaryUploadResult>>(
                        imageBytes, imageNames, contentType);
                    if (!uploadResponse.IsSuccess || uploadResponse.Data == null)
                        return ApiResponseDeserializer.FailResponse<ProductsVm>($"Upload failed: {uploadResponse.Message}");
                    uploadedImages = uploadResponse.Data;
                }

                // Set first image as primary
                for (int i = 0; i < uploadedImages.Count; i++)
                {
                    uploadedImages[i].IsPrimary = i == 0;
                }

                productsVm.Images = uploadedImages;

                var primary = uploadedImages.FirstOrDefault(i => i.IsPrimary);
                if (primary != null)
                {
                    productsVm.ImageUrl = primary.Url!;
                    productsVm.CloudPublicId = primary.CloudPublicId!;
                }

                var dto = productsVm.Map();
                var apiResponse = await _httpClient.PostAsJsonAsync("api/v1/Product/CreateProduct", dto);
                var json = await apiResponse.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<ProductsVm>(json);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<ProductsVm>(ex.Message);
            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<ApiResponse<List<ProductsVm>>> GetAllProductsAsync()
        {
            try
            {
                _loaderService.Show();
                var response = await _httpClient.GetAsync("api/v1/Product/GetAllProducts");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<List<ProductsVm>>(json);
            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<ApiResponse<ProductsVm>> GetProductByIdAsync(Guid productId)
        {
            try
            {
                _loaderService.Show();
                var response = await _httpClient.GetAsync($"api/v1/Product/GetProductById/{productId}");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<ProductsVm>(json);
            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<ApiResponse<string>> UpdateProducts(
           ProductsVm productsVm,
           List<byte[]>? newImageBytes = null,
           List<string>? newImageNames = null)
        {
            try
            {
                _loaderService.Show();

                // Upload new images first
                if (newImageBytes != null && newImageBytes.Any() &&
                    newImageNames != null && newImageNames.Any())
                {
                    string contentType = GetContentType(newImageNames[0]);

                    var uploadResponse =
                        await _cloudnaryManager.UploadToCloudinaryAsync<List<CloudinaryUploadResult>>(
                            newImageBytes, newImageNames, contentType);

                    if (!uploadResponse.IsSuccess || uploadResponse.Data == null)
                        return ApiResponseDeserializer.FailResponse<string>(
                            $"Image upload failed: {uploadResponse.Message}");

                    var uploadedImages = uploadResponse.Data;

                    foreach (var img in uploadedImages)
                    {
                        img.IsPrimary = productsVm.Images == null || !productsVm.Images.Any();
                    }

                    if (productsVm.Images == null)
                        productsVm.Images = new List<CloudinaryUploadResult>();

                    productsVm.Images.AddRange(uploadedImages);
                }

                // Ensure one primary
                if (productsVm.Images.Any() && !productsVm.Images.Any(i => i.IsPrimary))
                {
                    productsVm.Images.First().IsPrimary = true;
                }

                var primary = productsVm.Images.FirstOrDefault(i => i.IsPrimary);
                if (primary != null)
                {
                    productsVm.ImageUrl = primary.Url!;
                    productsVm.CloudPublicId = primary.CloudPublicId!;
                }

                var updateDto = productsVm.MapToUpdateDto();

                
                updateDto.DeletedImagePublicIds = productsVm.DeletedImagePublicIds;

                var apiResponse = await _httpClient.PutAsJsonAsync(
                    $"api/v1/Product/UpdateProduct/{productsVm.ProductId}",
                    updateDto);

                var json = await apiResponse.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<string>(json);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<string>(ex.Message);
            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<ApiResponse<bool>> RemoveProducts(Guid productId)
        {
            try
            {
                _loaderService.Show();
                var response = await _httpClient.DeleteAsync($"api/v1/Product/DeleteProduct/{productId}");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<bool>(json);
            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<ApiResponse<List<VmProductCategory>>> GetCategoriesAsync()
        {
            try
            {
                _loaderService.Show();
                var response = await _httpClient.GetAsync("api/v1/ProductRanking/GetProductCategory");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<List<VmProductCategory>>(json);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<List<VmProductCategory>>(ex.Message);
            }
            finally
            {
                _loaderService.Hide();
            }
        }
    }
}