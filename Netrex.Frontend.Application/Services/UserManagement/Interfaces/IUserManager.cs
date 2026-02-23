using Netrex.Frontend.Blazor.DTOs;
using Netrex.Frontend.Application.Commons.AppResponses;

namespace Netrex.Frontend.Application.Services.UserManagement.Interfaces
{
    public interface IUserManager
    {
        Task<ApiResponse<List<GetUsersDto>>> GetUsersAsync();
        Task<ApiResponse<bool>> DeleteUserAsync(Guid id);
        Task<ApiResponse<bool>> UpdateUserAsync(GetUsersDto user);
    }
}