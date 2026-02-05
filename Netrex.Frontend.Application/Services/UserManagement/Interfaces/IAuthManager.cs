using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;
using System.Net;

namespace Netrex.Frontend.Application.Services.UserManagement.Interfaces
{
    public interface IAuthManager
    {
        Task<ApiResponse<T>> RegisterAsync<T>(VmRegister registerView);
        Task<bool> LoginAsync(VmLogin viewModel);

        Task<ApiResponse<T>> VerifyOtpAsync<T>(VmVerifyOtp verifyOtp);  
        Task<ApiResponse<T>> ResendOtpAsync<T>(string email);
    }
}
