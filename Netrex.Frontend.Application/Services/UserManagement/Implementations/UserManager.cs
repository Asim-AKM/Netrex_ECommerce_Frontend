using Domain_Service.Enums;
using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement;
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

        public async Task<ApiResponse<List<VmUser>>> GetUsersAsync()
        {
            try
            {
                _loader.Show();
               
                var response = await _httpClient.GetAsync("api/User/getall");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<List<VmUser>>(json);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<List<VmUser>>(ex.Message);
            }
            finally
            {
                _loader.Hide();
            }
        }

        public async Task<ApiResponse<string>> UpdateUserStatusAsync(Guid id, UserStatus status)
        {
            try
            {
                _loader.Show();
                var payload = new { Id = id, Status = status };
                var response = await _httpClient.PutAsJsonAsync("api/User/updatestatus", payload);
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<string>(json);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<string>(ex.Message);
            }
            finally
            {
                _loader.Hide();
            }
        }
    }
}