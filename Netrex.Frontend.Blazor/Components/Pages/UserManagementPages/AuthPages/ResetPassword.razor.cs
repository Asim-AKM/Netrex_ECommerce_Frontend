using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Text.RegularExpressions;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class ResetPassword : IDisposable
    {
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        [Parameter] public string Email { get; set; } = string.Empty;

        // UI State Properties
        protected string Otp { get; set; } = "";
        protected string NewPassword { get; set; } = "";
        protected string ConfirmPassword { get; set; } = "";
        protected string? ErrorMessage { get; set; }
        protected string? ActiveErrorField { get; set; }
        protected bool IsProcessing { get; set; }
        protected bool IsSuccess { get; set; }
        protected string passwordType = "password";
        protected string eyeIcon = "assets/user-management-assets/icons/monkey-password-hide.png";

        // Timer Logic
        protected int Counter { get; set; } = 60;
        protected string TimerText => $"00:{Counter:D2}";
        protected bool IsTimerExpired => Counter <= 0;
        private System.Timers.Timer? _timer;

        protected override void OnInitialized()
        {
            StartTimer();
        }

        protected void HandleOtpInput(ChangeEventArgs e)
        {
            string rawValue = e.Value?.ToString() ?? "";
            // Keep only numbers
            Otp = Regex.Replace(rawValue, @"[^0-9]", "");
            ValidateWaterfall();
        }

        protected void StartTimer()
        {
            Counter = 60;
            _timer?.Dispose();
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += async (s, e) =>
            {
                if (Counter > 0)
                {
                    Counter--;
                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    _timer?.Stop();
                    await InvokeAsync(StateHasChanged);
                }
            };
            _timer.Start();
        }

        protected void ResendOtp()
        {
            IsSuccess = false;
            StartTimer();
            ActiveErrorField = null;
            ErrorMessage = null;
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

        protected void ValidateWaterfall()
        {
            ActiveErrorField = null;
            ErrorMessage = null;

            // 1. OTP Validation
            if (string.IsNullOrWhiteSpace(Otp))
            {
                ActiveErrorField = "otp"; ErrorMessage = "OTP is required."; return;
            }
            if (Otp.Length != 6)
            {
                ActiveErrorField = "otp"; ErrorMessage = "Exactly 6 digits required."; return;
            }
            if (IsTimerExpired && !IsSuccess)
            {
                ActiveErrorField = "otp"; ErrorMessage = "OTP has expired."; return;
            }

            // 2. Password Validation
            if (string.IsNullOrEmpty(NewPassword))
            {
                ActiveErrorField = "password"; ErrorMessage = "Password is required."; return;
            }
            if (NewPassword.Length < 8)
            {
                ActiveErrorField = "password"; ErrorMessage = "Must be at least 8 characters."; return;
            }
            if (!NewPassword.Any(char.IsUpper))
            {
                ActiveErrorField = "password"; ErrorMessage = "Need one Uppercase (A-Z)."; return;
            }
            if (!NewPassword.Any(char.IsDigit))
            {
                ActiveErrorField = "password"; ErrorMessage = "Need one Number (0-9)."; return;
            }

            // 3. Confirm Password Validation
            if (ConfirmPassword != NewPassword)
            {
                ActiveErrorField = "confirm"; ErrorMessage = "Passwords do not match."; return;
            }
        }

        protected async Task HandleReset()
        {
            ValidateWaterfall();

            if (ActiveErrorField == null)
            {
                IsProcessing = true;
                try
                {
                    await Task.Delay(1500); // Simulate API call
                    IsSuccess = true;
                    _timer?.Stop();
                    StateHasChanged();

                    await Task.Delay(2000); // Show success message before redirect
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

        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Dispose();
        }
    }
}