using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Blazor.Services;
using System.Text.RegularExpressions;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    // Class name must match the Razor file name (VerifyEmail)
    public partial class VerifyEmail : IDisposable
    {
        [Inject] public ToastService _toastService { get; set; } = default!;
        [Inject] public NavigationManager Navigation { get; set; } = default!;

        protected string Email { get; set; } = "";
        protected string? ErrorMessage { get; set; }
        protected bool IsProcessing { get; set; }

        public async Task HandleRegister()
        {
            ErrorMessage = null;

            if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ErrorMessage = "Please enter a valid email address.";
                return;
            }

            IsProcessing = true;
            try
            {
                // Simulate API call
                await Task.Delay(1500);

                _toastService.Success("OTP Sent Successfully!");

                // Navigating to OTP verification
                Navigation.NavigateTo($"/verifyotp/{Uri.EscapeDataString(Email)}");
            }
            catch (Exception ex)
            {
                _toastService.Error("Error: " + ex.Message);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        public void Dispose() { }
    }
}