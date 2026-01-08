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
        public async Task<HttpStatusCode> RegisterAsync(VmRegister registerView)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Authentication/Create", registerView);
            return response.StatusCode;
        }

        public async Task<bool> LoginAsync(VmLogin viewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Authentication/Login", viewModel);
            return response.IsSuccessStatusCode;
        }
    }
}
