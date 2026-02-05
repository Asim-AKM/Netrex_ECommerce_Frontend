using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.ProductManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.ProductManagement;
using Netrex.Frontend.Blazor.Services;
using System.Net.Http;
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

        public async Task<ApiResponse<ProductsVm>> AddProducts(
            ProductsVm productsVm,
            List<byte[]> imageBytes,
            List<string> imageNames)
        {
            try
            {
                _loaderService.Show();

                for (int i = 0; i < imageBytes.Count; i++)
                {
                    var uploadResponse =
                        await _cloudnaryManager.UploadImageToCloudinary(
                            imageBytes[i],
                            imageNames[i],
                            GetContentType(imageNames[i])
                        );

                    if (!uploadResponse.IsSuccess)
                    {
                        return new ApiResponse<ProductsVm>
                        {
                            IsSuccess = false,
                            IsError = true,
                            Message = $"Image {imageNames[i]} upload failed",
                            Status = 500,
                            Data = default!
                        };
                    }

                   
                    if (i == 0)
                    {
                        productsVm.ImageUrl = uploadResponse.Data.Url;
                        productsVm.ImagePublicId = uploadResponse.Data.PublicId;
                        productsVm.IsPrimary = true;
                    }
                }

               
                var response =
                    await _httpClient.PostAsJsonAsync(
                        "api/Product/CreateProduct",
                        productsVm
                    );

                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<ProductsVm>(json);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductsVm>
                {
                    IsSuccess = false,
                    IsError = true,
                    Message = ex.Message,
                    Status = 500,
                    Data = default!
                };
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
                var f when f.EndsWith(".jpg") || f.EndsWith(".jpeg") => "image/jpeg",
                var f when f.EndsWith(".png") => "image/png",
                var f when f.EndsWith(".gif") => "image/gif",
                _ => "application/octet-stream"
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
