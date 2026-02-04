using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;
using System.Timers;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class ResetPassword : IDisposable
    {
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        private string Otp { get; set; } = "";
        private string NewPassword { get; set; } = "";
        private string ConfirmPassword { get; set; } = "";
        private string? ErrorMessage { get; set; }
        private string? ActiveErrorField { get; set; }
        private bool IsProcessing { get; set; }
        private bool IsSuccess { get; set; }

        private string passwordType = "password";
        private string eyeIcon = "assets/user-management-assets/icons/monkey-password-hide.png";

        // Timer Logic
        private int Counter { get; set; } = 60;
        private string TimerText => $"00:{Counter:D2}";
        private bool IsTimerExpired => Counter <= 0;
        private System.Timers.Timer? _timer;

        protected override void OnInitialized() => StartTimer();

        private void HandleOtpInput(ChangeEventArgs e)
        {
            string rawValue = e.Value?.ToString() ?? "";
            // Sirf numbers allow honge
            Otp = Regex.Replace(rawValue, @"[^0-9]", "");
            ValidateWaterfall();
        }

        private void ValidateWaterfall()
        {
            ActiveErrorField = null;
            ErrorMessage = null;

            // 1. OTP Check
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

            // 2. Password Check
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
            if (!Regex.IsMatch(NewPassword, @"[!@#$%^&*()]"))
            {
                ActiveErrorField = "password"; ErrorMessage = "Need one Special Character."; return;
            }

            // 3. Confirm Check
            if (ConfirmPassword != NewPassword)
            {
                ActiveErrorField = "confirm"; ErrorMessage = "Passwords do not match."; return;
            }
        }

        private async Task HandleReset()
        {
            ValidateWaterfall();
            if (ActiveErrorField == null)
            {
                IsProcessing = true;
                await Task.Delay(1500); // Fake API Call
                IsSuccess = true;
                _timer?.Stop();
                IsProcessing = false;
                StateHasChanged();
                await Task.Delay(2000);
                Navigation.NavigateTo("/login");
            }
        }

        private void StartTimer()
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
                    _timer.Stop();
                    await InvokeAsync(StateHasChanged);
                }
            };
            _timer.Start();
        }

        private void ResendOtp() { IsSuccess = false; StartTimer(); ActiveErrorField = null; }

        private void TogglePassword()
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
            _timer?.Stop();
            _timer?.Dispose();
        }
    }
}