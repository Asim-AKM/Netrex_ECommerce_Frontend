using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;
using Netrex.Frontend.Blazor.Services;
using System.Net;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class Login
    {
        [Inject] private IAuthManager? AuthManager { get; set; } = default!;
        [Inject] private ToastService Toast { get; set; } = default!;
        [Inject] private LoaderService Loader { get; set; } = default!;
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        private readonly VmLogin _model = new VmLogin();
        private bool rememberMe = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
        public async Task HandleLogin()
        {
            try
            {
                Loader.Show();
                var result = await AuthManager!.LoginAsync(_model);
                HandleApiResponse(result);
                if (!result.IsSuccess || result.Data == null)
                {
                    if (result.Status == (int)HttpStatusCode.InternalServerError)
                    {
                        Toast.Error("Internal Server Error", "Login failed");
                        return;
                    }
                    Toast.Error(result.Message ?? "Login failed", "Error");
                    return;
                }

                await JSRuntime.InvokeVoidAsync("submitLoginForm", result.Data, rememberMe);

            }
            catch (Exception ex)
            {
                Toast.Error("Login failed: " + ex.Message, "Error");
            }
            finally { Loader.Hide(); }
        }
    }
}