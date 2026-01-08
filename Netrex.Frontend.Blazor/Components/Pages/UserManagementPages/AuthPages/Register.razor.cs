using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class Register
    {
        [Inject]
        private IAuthManager _authManager { get; set; }
        RegisterViewModel _model = new RegisterViewModel();
        public async Task HandleSignUp()
        {
            await _authManager.RegisterAsync(_model);
        }
    }
}
