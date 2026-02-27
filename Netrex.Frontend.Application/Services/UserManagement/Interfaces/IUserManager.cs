using Domain_Service.Enums;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.ViewModels.UserManagement;
using Netrex.Frontend.Blazor.DTOs;

namespace Netrex.Frontend.Application.Services.UserManagement.Interfaces
{
    public interface IUserManager
    {
        Task<ApiResponse<List<VmUser>>> GetUsersAsync();
        Task<ApiResponse<string>> UpdateUserStatusAsync(Guid id, UserStatus status);
    }
}