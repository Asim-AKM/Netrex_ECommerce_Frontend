using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.SellerAndShop.Interfaces;
using Netrex.Frontend.Blazor.Services;
using System.Net.Http.Json;

namespace Netrex.Frontend.Application.Services.SellerAndShop.Implementations
{
    public class SellerManager : ISellerManager
    {
        private readonly HttpClient _httpClient;
        private readonly LoaderService _loader;
        public SellerManager(IHttpClientFactory httpClient, LoaderService loaderService)
        {
            _httpClient = httpClient.CreateClient("ApiClient");
            _loader = loaderService;
        }
        public async Task<ApiResponse<VmSeller>> CreateSellerAsync(VmSeller vmSeller)
        {
            try
            {
                _loader.Show();
                var response = await _httpClient.PostAsJsonAsync("api/Seller/CreateSeller", vmSeller);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return ApiResponseDeserializer.FailResponse<VmSeller>(
                        $"Failed to add seller. Status: {response.StatusCode}");
                }

                return ApiResponseDeserializer.Deserialize<VmSeller>(jsonString);
            }
            catch (Exception ex)
            {

                return ApiResponseDeserializer.FailResponse<VmSeller>(
            $"Unexpected error occurred: {ex.Message}");
            }
            finally
            {
                _loader.Hide();
            }
        }
        public async Task<ApiResponse<string>> DeleteSellerAsync(Guid Id)
        {
            try
            {
                _loader.Show();
                var response = await _httpClient.DeleteAsync($"api/Seller/DeleteSeller/{Id}");
                var jsonString = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return ApiResponseDeserializer.FailResponse<string>($"Failed to delete seller. Status: {response.StatusCode}");
                }
                return ApiResponseDeserializer.Deserialize<string>(jsonString);
            }
            catch(Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<string>(
           $"Unexpected error occurred: {ex.Message}");
            }
            finally
            {
                _loader.Hide();
            }
        }
        public async Task<ApiResponse<VmSeller>> GetSellerbyIdAsync(Guid Id)
        {
            try
            {
                _loader.Show();
                var response = await _httpClient.GetAsync($"api/Seller/GetSellerById{Id}");
                var jsonString = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return ApiResponseDeserializer.FailResponse<VmSeller>($"Failed to retrieve seller.Status:{response.StatusCode}");
                }
                return ApiResponseDeserializer.Deserialize<VmSeller>(jsonString);
            }
            catch(Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<VmSeller>($"Unexpected error occurred: {ex.Message}");
            }
            finally
            {
                _loader.Hide();
            }
        }
        public async Task<ApiResponse<List<VmSeller>>> GetSellerAsync()
        {
            try
            {
                _loader.Show();

                var response = await _httpClient.GetAsync("api/Seller/GetAllSellers");
                var jsonString=await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return ApiResponseDeserializer.FailResponse<List<VmSeller>>("Failed to retrieve sellers. ");
                }
                return ApiResponseDeserializer.Deserialize<List<VmSeller>>(jsonString);
            }
            catch( Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<List<VmSeller>>($"Error retrieving sellers: {ex.Message}");
            }
            finally
            {
                _loader.Hide();
            }
        }
        public async Task<ApiResponse<VmSeller>> UpdateSellerAsync(VmSeller vmSeller)
        {
            try
            {
                _loader.Show();

                var response = await _httpClient.PutAsJsonAsync(
                    $"api/Product/UpdateSeller/{vmSeller.SellerId}", vmSeller);
                var jsonString=await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return ApiResponseDeserializer.FailResponse<VmSeller>($"Failed to update seller. Status: {response.StatusCode}");
                }
                return ApiResponseDeserializer.Deserialize<VmSeller>(jsonString);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<VmSeller>(
                    $"Unexpected error occurred: {ex.Message}");
            }
            finally
            {
                _loader.Hide();
            }
        }
    }
}
