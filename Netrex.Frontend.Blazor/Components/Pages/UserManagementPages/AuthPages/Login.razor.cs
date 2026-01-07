using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class Login
    {
        [Inject]
        public IAuthManager? _authManager { get; set; }
        LoginViewModel _model = new LoginViewModel();
        public async Task HandleLogin()
        {
            await _authManager!.LoginAsync(_model);
        }
    }
}
