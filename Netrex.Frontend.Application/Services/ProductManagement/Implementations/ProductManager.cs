using Netrex.Frontend.Application.Commons.AppResponses;
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

        public ProductManager(IHttpClientFactory httpClientFactory, LoaderService loaderService)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _loaderService = loaderService;
        }
        public async Task<ProductsVm> AddProducts(ProductsVm productsVm)
        {
            try
            {
                _loaderService.Show();

                var response = await _httpClient.PostAsJsonAsync(
                    "api/Product/CreateProduct", productsVm);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ProductsVm>();
                    return result!;
                }
                else
                {
                    throw new Exception("Failed to add product.");
                }
            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<IEnumerable<ProductsVm>> GetAllProductsAsync()
        {
            try
            {
                _loaderService.Show();

                var response = await _httpClient.GetAsync(
                    "api/Product/GetAllProducts");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to retrieve products.");
                }

                var products = await response.Content
                    .ReadFromJsonAsync<IEnumerable<ProductsVm>>();

                return products ?? Enumerable.Empty<ProductsVm>();
            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<ProductsVm> GetProductByIdAsync(int productId)
        {
            try
            {
                _loaderService.Show();

                var response = await _httpClient.GetAsync(
                    $"api/Product/GetProductById/{productId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to retrieve product.");
                }

                var result = await response.Content
                    .ReadFromJsonAsync<ProductsVm>();

                return result!;
            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<bool> RemoveProducts(int productId)
        {
            try
            {
                _loaderService.Show();
                var response = await _httpClient.DeleteAsync(
                    $"api/Product/DeleteProduct/{productId}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to delete product.");
                }
                return true;

            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<ProductsVm> UpdateProducts(ProductsVm productsVm)
        {
            try
            {
                _loaderService.Show();

                var response = await _httpClient.PutAsJsonAsync(
                    $"api/Product/UpdateProduct/{productsVm.ProductId}",
                    productsVm);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to update product.");
                }

                var apiResponse = await response.Content
                    .ReadFromJsonAsync<ApiResponse<ProductsVm>>();

                return apiResponse!.Data;
            }
            finally
            {
                _loaderService.Hide();
            }
        }
    }
}
