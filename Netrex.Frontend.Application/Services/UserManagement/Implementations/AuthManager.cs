using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;
using System.Net.Http.Json;


namespace Netrex.Frontend.Application.Services.UserManagement.Implementations
{
    public class AuthManager : IAuthManager
    {
        private readonly HttpClient _httpClient;
        public AuthManager(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Netrex");
        }
        public async Task<bool> RegisterAsync(RegisterViewModel registerView)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Authentication/Create", registerView);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(LoginViewModel viewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Authentication/Login", viewModel);
            return response.IsSuccessStatusCode;
        }
    }
}
