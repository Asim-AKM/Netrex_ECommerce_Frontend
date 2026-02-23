using Netrex.Frontend.Blazor.DTOs;
using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Blazor.Services;
using System.Net.Http.Json;

namespace Netrex.Frontend.Application.Services.UserManagement.Implementations
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;
        private readonly LoaderService _loader;

        public UserManager(IHttpClientFactory factory, LoaderService loader)
        {
            _httpClient = factory.CreateClient("ApiClient");
            _loader = loader;
        }

        public async Task<ApiResponse<List<GetUsersDto>>> GetUsersAsync()
        {
            try
            {
                _loader.Show();

                var response = await _httpClient.GetAsync("api/UserManagement/GetUsers");
                var json = await response.Content.ReadAsStringAsync();

                return ApiResponseDeserializer.Deserialize<List<GetUsersDto>>(json);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<List<GetUsersDto>>(ex.Message);
            }
            finally
            {
                _loader.Hide();
            }
            
        }

        public async Task<ApiResponse<bool>> DeleteUserAsync(Guid id)
        {
            try
            {
                _loader.Show();

                var response = await _httpClient.DeleteAsync($"api/UserManagement/DeleteUser/{id}");
                var json = await response.Content.ReadAsStringAsync();

                return ApiResponseDeserializer.Deserialize<bool>(json);
            }
            finally
            {
                _loader.Hide();
            }
        }

        public async Task<ApiResponse<bool>> UpdateUserAsync(GetUsersDto user)
        {
            try
            {
                _loader.Show();

                var response = await _httpClient.PutAsJsonAsync("api/UserManagement/UpdateUser", user);
                var json = await response.Content.ReadAsStringAsync();

                return ApiResponseDeserializer.Deserialize<bool>(json);
            }
            finally
            {
                _loader.Hide();
            }
        }
    }
}