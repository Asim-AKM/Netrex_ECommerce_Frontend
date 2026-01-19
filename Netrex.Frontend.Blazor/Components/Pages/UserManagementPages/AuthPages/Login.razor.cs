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
            try
            {
                _loader.Show(); // Loader start karein

                // Asal login process yahan hoga
                // await _authManager!.LoginAsync(_model);

                // Sirf demonstration ke liye delay (taki aap loader dekh sakein)
                await Task.Delay(2000);

                _toastMan.Success("Login Successfully!", "Success");
            }
            catch (Exception ex)
            {
                _toastMan.Error("Login failed: " + ex.Message, "Error");
            }
            finally
            {
                _loader.Hide(); // Task khatam hotay hi loader stop ho jayega (Success ho ya Error)
            }
        }
    }
}