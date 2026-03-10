using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.ProductManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.ProductManagement;
using Netrex.Frontend.Blazor.Services;

namespace Netrex.Frontend.Application.Services.ProductManagement.Implementations
{
    public class ProductRanking : IProductRanking
    {
        private readonly LoaderService _loaderService;

        private readonly HttpClient _httpClient;
        public ProductRanking(IHttpClientFactory httpClientFactory, LoaderService loaderService)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _loaderService = loaderService;
        }
        public async Task<ApiResponse<List<ProductsVm>>> GetBestSellersAsync()
        {
            try
            {
                _loaderService.Show();

                var response = await _httpClient.GetAsync("ProductRanking/best-sellers");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<List<ProductsVm>>(json);
            }
            finally
            {
                _loaderService.Hide();
            }
        }
        public async Task<ApiResponse<List<ProductsVm>>> GetHomepageProductsAsync(Guid? categoryid = null,int pageNumber = 1,   int pageSize = 10)
        {
            try
            {
                //_loaderService.Show();

                var url = $"ProductRanking/homepage?pageNumber={pageNumber}&pageSize={pageSize}";

                if (categoryid.HasValue && categoryid != Guid.Empty)
                {
                    url += $"&categoryid={categoryid}";
                }

                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                return ApiResponseDeserializer.Deserialize<List<ProductsVm>>(json);
            }
            finally
            {
                //_loaderService.Hide();
            }
        }
        public async Task<ApiResponse<List<ProductsVm>>> GetNewArrivalsAsync()
        {
            try
            {
                _loaderService.Show();

                var response = await _httpClient.GetAsync("ProductRanking/new-arrivals");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<List<ProductsVm>>(json);
            }
            finally
            {
                _loaderService.Hide();
            }
        }
        public async Task<ApiResponse<List<ProductsVm>>> GetTopRatedAsync()
        {
            try
            {
                _loaderService.Show();

                var response = await _httpClient.GetAsync("ProductRanking/top-rated");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<List<ProductsVm>>(json);
            }
            finally
            {
                _loaderService.Hide();
            }
        }
        public async Task<ApiResponse<List<ProductsVm>>> GetTrendingAsync()
        {
            try
            {
                _loaderService.Show();
                var response = await _httpClient.GetAsync("ProductRanking/trending");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<List<ProductsVm>>(json);

            }
            finally
            {
                _loaderService.Hide();
            }
        }
    }
}
