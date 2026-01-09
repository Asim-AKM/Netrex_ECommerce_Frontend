using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;
using System.Net;
using System.Net.Http.Json;


namespace Netrex.Frontend.Application.Services.UserManagement.Implementations
{
    public class AuthManager : IAuthManager
    {
        private readonly HttpClient _httpClient;
        public AuthManager(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<ApiResponse<T>> RegisterAsync<T>(VmRegister registerView)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/Authentication/Create",
                registerView
            );

            var json = await response.Content.ReadAsStringAsync();

            return ApiResponseDeserializer.Deserialize<T>(json);
        }



        public async Task<bool> LoginAsync(VmLogin viewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Authentication/Login", viewModel);
            return response.IsSuccessStatusCode;
        }
    }
}
