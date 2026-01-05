using Netrex.Frontend.Blazor.Models;

namespace Netrex.Frontend.Blazor.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        public async Task<bool> UserRegisterAsync(VMUserRegistration obj)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Authentication/create", obj);
            return response.IsSuccessStatusCode;
        }
    }
}
