using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;
using System.Net;
using System.Net.Http.Json;
// LoaderService ka namespace add karein (Project reference check kar lein)
using Netrex.Frontend.Blazor.Services;


namespace Netrex.Frontend.Application.Services.UserManagement.Implementations
{
    public class AuthManager : IAuthManager
    {
        private readonly HttpClient _httpClient;
        private readonly LoaderService _loader; // 1. LoaderService field add karein
        // 2. Constructor mein LoaderService inject karein
        public AuthManager(IHttpClientFactory httpClientFactory, LoaderService loader)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _loader = loader;
        }
        public async Task<ApiResponse<T>> RegisterAsync<T>(VmRegister registerView)
        {
            try
            {
                // 3. Loader show karein
                _loader.Show();

                // Testing ke liye optional delay
                await Task.Delay(2000);

                var response = await _httpClient.PostAsJsonAsync(
                    "Authentication/Register",
                    registerView
                );

                var json = await response.Content.ReadAsStringAsync();

                return ApiResponseDeserializer.Deserialize<T>(json);
            }
            finally
            {
                // 4. Loader hide karein (chahe success ho ya error)
                _loader.Hide();
            }
        }
        public async Task<ApiResponse<string>> LoginAsync(VmLogin viewModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Authentication/SignIn", viewModel);
                return ApiResponseDeserializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<string>(ex.Message);
            }
        }
        public async Task<ApiResponse<T>> VerifyOtpAsync<T>(VmVerifyOtp verifyOtp)
        {
            try
            {
                _loader.Show();

                var response = await _httpClient.PostAsJsonAsync(
                    "Authentication/VerifyEmail",
                    verifyOtp
                );

                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<T>(json);
            }
            finally
            {
                _loader.Hide();
            }
        }
        public async Task<ApiResponse<T>> ResendOtpAsync<T>(string email)
        {
            try
            {
                _loader.Show();

                var response = await _httpClient.PostAsJsonAsync(
                    "Authentication/ResendOtp",
                    email
                );

                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<T>(json);
            }
            finally
            {
                _loader.Hide();
            }
        }

        public async Task<ApiResponse<string>> RefreshTokenAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("UserSession/GetRefreshJwt");
                return ApiResponseDeserializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<string>(ex.Message);
            }
        }
    }
}
