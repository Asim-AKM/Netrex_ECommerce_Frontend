using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Blazor.Services;
using Netrex.Frontend.Application.Commons.Enums;

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
        public ToastService _toastService { get; set; }
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

               // _toastService.Success("Login Successfully!", "Success");
               // _toastService.Notify().WithType(ToastType.Payment).WithMessage("Payment Sent To Saad The Great").WithTitle("Payment Succeed").Show();
                _toastService.CardUpdated("Hp Leptop 8 256 is Added to Cart");
            }
            catch (Exception ex)
            {
                _toastService.Error("Login failed: " + ex.Message, "Error");
            }
            finally
            {
                _loader.Hide(); // Task khatam hotay hi loader stop ho jayega (Success ho ya Error)
            }
        }
    }
}