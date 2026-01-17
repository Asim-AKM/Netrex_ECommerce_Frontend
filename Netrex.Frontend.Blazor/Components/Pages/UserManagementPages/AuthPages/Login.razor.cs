using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Blazor.Services;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class Login
    {
        public LoaderService _loader;
        public Login(LoaderService loader)
        {
            this._loader = loader;
        }
        [Inject]
        public IAuthManager? _authManager { get; set; }
        [Inject]
        public ToastManager _toastMan { get; set; }
        VmLogin _model = new VmLogin();
        public async Task HandleLogin()
        {
            _loader.Show();
            _toastMan.Success("Login Succesfuly !", "Succes");
            //await _authManager!.LoginAsync(_model);
        }
    }
}
