using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Text.RegularExpressions;
using System.Linq;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class ResetPassword : IDisposable
    {
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        [Parameter] public string Email { get; set; } = string.Empty;

        // UI Binding Properties
        protected string NewPassword { get; set; } = "";
        protected string ConfirmPassword { get; set; } = "";
        protected string? ErrorMessage { get; set; }
        protected string? ActiveErrorField { get; set; }
        protected bool IsProcessing { get; set; }
        protected bool IsSuccess { get; set; } // Success alert dikhane ke liye

        // Password Visibility Logic
        protected string passwordType = "password";
        protected string eyeIcon = "assets/user-management-assets/icons/monkey-password-hide.png";

        protected async Task HandleReset()
        {
            ValidateWaterfall();

            if (ActiveErrorField == null)
            {
                IsProcessing = true;
                ErrorMessage = null;
                try
                {
                    // Yahan aapki API call aayegi password update karne ke liye
                    await Task.Delay(1500); // Simulation

                    IsSuccess = true;
                    StateHasChanged();

                    // 2 second baad login par redirect karein
                    await Task.Delay(2000);
                    Navigation.NavigateTo("/login");
                }
                catch (Exception)
                {
                    ErrorMessage = "Something went wrong. Please try again.";
                }
                finally
                {
                    IsProcessing = false;
                }
            }
        }

        private void ValidateWaterfall()
        {
            ActiveErrorField = null;
            ErrorMessage = null;

            // 1. New Password Validation
            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                ActiveErrorField = "password";
                ErrorMessage = "Password is required.";
                return;
            }
            if (NewPassword.Length < 8)
            {
                ActiveErrorField = "password";
                ErrorMessage = "At least 8 characters.";
                return;
            }
            if (!NewPassword.Any(char.IsUpper))
            {
                ActiveErrorField = "password";
                ErrorMessage = "Need one Uppercase (A-Z).";
                return;
            }
            if (!NewPassword.Any(char.IsDigit))
            {
                ActiveErrorField = "password";
                ErrorMessage = "Need one Number (0-9).";
                return;
            }

            // 2. Confirm Password Match
            if (ConfirmPassword != NewPassword)
            {
                ActiveErrorField = "confirm";
                ErrorMessage = "Passwords do not match.";
                return;
            }
        }

        protected void TogglePassword()
        {
            if (passwordType == "password")
            {
                passwordType = "text";
                eyeIcon = "assets/user-management-assets/icons/monkey-password-show.png";
            }
            else
            {
                passwordType = "password";
                eyeIcon = "assets/user-management-assets/icons/monkey-password-hide.png";
            }
        }

        public void Dispose()
        {
            // Timer cleanup ki yahan zarurat nahi hai kyunki timer khatam kar diya gaya hai
        }
    }
}