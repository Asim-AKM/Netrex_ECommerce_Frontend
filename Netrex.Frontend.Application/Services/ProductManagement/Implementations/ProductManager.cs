using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Commons.Mappers.Products;
using Netrex.Frontend.Application.DTO_s.ProductDto;
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

        public async Task<ApiResponse<ProductsVm>> AddProducts(ProductsVm productsVm, List<byte[]> imageBytes, List<string> imageNames)
        {
            try
            {
                _loaderService.Show();


                string contentType = imageNames.Count > 0
                    ? GetContentType(imageNames[0])
                    : "image/jpeg";

                List<CloudinaryUploadResult> uploadedImages;

                if (imageBytes.Count == 1)
                {

                    var singleResponse = await _cloudnaryManager.UploadToCloudinaryAsync<CloudinaryUploadResult>(
                        imageBytes, imageNames, contentType);

                    if (!singleResponse.IsSuccess || singleResponse.Data == null)
                    {
                        return ApiResponseDeserializer.FailResponse<ProductsVm>(
                            $"Single image upload failed: {singleResponse.Message}");
                    }

                    uploadedImages = new List<CloudinaryUploadResult> { singleResponse.Data };
                }
                else
                {

                    var listResponse = await _cloudnaryManager.UploadToCloudinaryAsync<List<CloudinaryUploadResult>>(
                        imageBytes, imageNames, contentType);

                    if (!listResponse.IsSuccess || listResponse.Data == null)
                    {
                        return ApiResponseDeserializer.FailResponse<ProductsVm>(
                            $"Multiple images upload failed: {listResponse.Message}");
                    }

                    uploadedImages = listResponse.Data;
                }

                if (uploadedImages.Count == 0)
                {
                    return ApiResponseDeserializer.FailResponse<ProductsVm>(
                        "No images uploaded successfully");
                }

                var firstImage = uploadedImages[0];
                productsVm.ImageUrl = firstImage.Url!;
                productsVm.CloudPublicId = firstImage.CloudPublicId!;
                productsVm.IsPrimary = true;

                var AddProductDto = productsVm.Map();

                var response = await _httpClient.PostAsJsonAsync(
                    "api/Product/CreateProduct",
                    AddProductDto
                );

                var json = await response.Content.ReadAsStringAsync();
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

        private string GetContentType(string fileName)
        {
            return fileName.ToLower() switch
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


        public async Task<ApiResponse<IEnumerable<ProductsVm>>> GetAllProductsAsync()
        {
            try
            {
                _loaderService.Show();
                var response = await _httpClient.GetAsync("api/Product/GetAllProducts");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<IEnumerable<ProductsVm>>(json);
            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<ApiResponse<ProductsVm>> GetProductByIdAsync(int productId)
        {
            try
            {
                _loaderService.Show();
                var response = await _httpClient.GetAsync($"api/Product/GetProductById/{productId}");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<ProductsVm>(json);
            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<ApiResponse<bool>> RemoveProducts(int productId)
        {
            try
            {
                _loaderService.Show();
                var response = await _httpClient.DeleteAsync($"api/Product/DeleteProduct/{productId}");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<bool>(json);
            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<ApiResponse<ProductsVm>> UpdateProducts(ProductsVm productsVm)
        {
            try
            {
                _loaderService.Show();
                var response = await _httpClient.PutAsJsonAsync(
                    $"api/Product/UpdateProduct/{productsVm.ProductId}",
                    productsVm);
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<ProductsVm>(json);
            }
            finally
            {
                _loaderService.Hide();
            }
        }
    }
}
