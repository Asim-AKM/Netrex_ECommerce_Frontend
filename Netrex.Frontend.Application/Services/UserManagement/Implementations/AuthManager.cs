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
                    "api/Authentication/Create",
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



        public async Task<bool> LoginAsync(VmLogin viewModel)
        {
            try
            {
                _loader.Show();

                var response = await _httpClient.PostAsJsonAsync("api/Authentication/Login", viewModel);
                return response.IsSuccessStatusCode;
            }
            finally
            {
                _loader.Hide();
            }
        }
    }
}
