using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;
using System.Net;

namespace Netrex.Frontend.Application.Services.UserManagement.Interfaces
{
    public interface IAuthManager
    {
        Task<HttpStatusCode> RegisterAsync(VmRegister registerView);
        Task<bool> LoginAsync(VmLogin viewModel);
    }
}
