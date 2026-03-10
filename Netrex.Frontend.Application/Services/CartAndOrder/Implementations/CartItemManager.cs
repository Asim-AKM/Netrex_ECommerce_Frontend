using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;
using Netrex.Frontend.Blazor.Services;
using System.Net.Http.Json;

namespace Netrex.Frontend.Application.Services.CartAndOrder.Implementations
{
    public class CartItemManager : ICartItemManager
    {
        private readonly HttpClient _httpClient;
        private readonly LoaderService _loader;

        public CartItemManager(IHttpClientFactory httpClientFactory, LoaderService loader)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _loader = loader;
        }

        public async Task<ApiResponse<List<VmGetCartItem>>> GetCartItemAsync()
        {
            try
            {
                _loader.Show();

                var response = await _httpClient.GetAsync("/api/v1/CartItem");
                var json = await response.Content.ReadAsStringAsync();

                return ApiResponseDeserializer.Deserialize<List<VmGetCartItem>>(json);
            }
            finally
            {
                _loader.Hide();
            }
        }

        public async Task<ApiResponse<bool>> AddCartItemAsync(VmAddCartItem vm)
        {
            try
            {

                _loader.Show();

                var response = await _httpClient.PostAsJsonAsync("api/CartItem/", vm);

                var json = await response.Content.ReadAsStringAsync();

                return ApiResponseDeserializer.Deserialize<bool>(json);
            }
            finally
            {

                _loader.Hide();
            }
        }

        public async Task<ApiResponse<bool>> DecreaseQuantityAsync(Guid cartitemid)
        {
            var response = await _httpClient.PutAsync($"api/CartItem/DecreaseQuantity/{cartitemid}", null);
            return new ApiResponse<bool>
            {
                IsSuccess = response.IsSuccessStatusCode,
                Data = response.IsSuccessStatusCode
            };
        }

        public async Task<ApiResponse<bool>> IncreaseQuantityAsync(Guid cartitemid)
        {
            var response = await _httpClient.PutAsync($"api/CartItem/IncreaseQuantity/{cartitemid}", null);
            return new ApiResponse<bool>
            {
                IsSuccess = response.IsSuccessStatusCode,
                Data = response.IsSuccessStatusCode
            };

        }

        public async Task<ApiResponse<bool>> RemoveItemAsync(Guid cartitemid)
        {
            var response = await _httpClient.DeleteAsync($"api/CartItem/{cartitemid}");
            return new ApiResponse<bool>
            {
                IsSuccess = response.IsSuccessStatusCode,
                Data = response.IsSuccessStatusCode
            };
        }
    }
}
