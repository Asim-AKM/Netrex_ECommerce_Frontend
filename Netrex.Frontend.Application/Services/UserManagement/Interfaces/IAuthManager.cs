using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;

namespace Netrex.Frontend.Application.Services.UserManagement.Interfaces
{
    public interface IAuthManager
    {
        Task<bool> RegisterAsync(RegisterViewModel registerView);
        Task<bool> LoginAsync(LoginViewModel viewModel);
    }
}
