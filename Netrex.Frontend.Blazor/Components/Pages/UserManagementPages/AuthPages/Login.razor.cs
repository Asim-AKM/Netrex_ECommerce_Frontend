using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;
using Netrex.Frontend.Application.Services.Common;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class Login
    {
        [Inject]
        public IAuthManager? _authManager { get; set; }
        [Inject]
        public ToastManager _toastMan { get; set; }
        VmLogin _model = new VmLogin();
        public async Task HandleLogin()
        {
            _toastMan.Success("Login Succesfuly !", "Succes");
            //await _authManager!.LoginAsync(_model);
        }
    }
}
