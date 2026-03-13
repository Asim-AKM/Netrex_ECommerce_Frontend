using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Commons.SharedClasses;
using Netrex.Frontend.Application.ViewModels.WishList;
using System.Net;
using System.Net.Http.Json;

namespace Netrex.Frontend.Application.Services.WishList
{
    public class WishListManager : BaseManager, IWishListManager
    {
        private readonly HttpClient _http;

        public WishListManager(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<ApiResponse<List<VmGetWishListItem>>> GetWishListItemsAsync(Guid userId)
        {
            try
            {
                var response = await _http.GetAsync($"WishList/WishListItem/{userId}");
                if (TryGetErrorResponse<List<VmGetWishListItem>>(response, out var errorResponse))
                    return errorResponse;
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<List<VmGetWishListItem>>(json);
            }
            catch (HttpRequestException)
            {
                return ApiResponseDeserializer.FailResponse<List<VmGetWishListItem>>("network_error", HttpStatusCode.ServiceUnavailable);
            }
            catch (TaskCanceledException)
            {
                return ApiResponseDeserializer.FailResponse<List<VmGetWishListItem>>("network_error", HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<List<VmGetWishListItem>>(ex.Message);
            }
        }

        public async Task<ApiResponse<Guid>> AddWishListItemAsync(VmAddWishListItem request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("WishList/WishListItem", request);
                if (TryGetErrorResponse<Guid>(response, out var errorResponse))
                    return errorResponse;
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<Guid>(json);
            }
            catch (HttpRequestException)
            {
                return ApiResponseDeserializer.FailResponse<Guid>("network_error", HttpStatusCode.ServiceUnavailable);
            }
            catch (TaskCanceledException)
            {
                return ApiResponseDeserializer.FailResponse<Guid>("network_error", HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<Guid>(ex.Message);
            }
        }

        public async Task<ApiResponse<string>> DeleteWishListItemAsync(Guid wishListItemId)
        {
            try
            {
                var response = await _http.DeleteAsync($"WishList/WishListItem/{wishListItemId}");
                if (TryGetErrorResponse<string>(response, out var errorResponse))
                    return errorResponse;
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<string>(json);
            }
            catch (HttpRequestException)
            {
                return ApiResponseDeserializer.FailResponse<string>("network_error", HttpStatusCode.ServiceUnavailable);
            }
            catch (TaskCanceledException)
            {
                return ApiResponseDeserializer.FailResponse<string>("network_error", HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<string>(ex.Message);
            }
        }

        public async Task<ApiResponse<int>> GetWishListCountAsync(Guid userId)
        {
            try
            {
                var response = await _http.GetAsync($"WishList/WishListCount/{userId}");
                if (TryGetErrorResponse<int>(response, out var errorResponse))
                    return errorResponse;
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<int>(json);
            }
            catch (HttpRequestException)
            {
                return ApiResponseDeserializer.FailResponse<int>("network_error", HttpStatusCode.ServiceUnavailable);
            }
            catch (TaskCanceledException)
            {
                return ApiResponseDeserializer.FailResponse<int>("network_error", HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<int>(ex.Message);
            }
        }
    }
}