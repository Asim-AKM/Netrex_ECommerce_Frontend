//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Web;
//using System.Text.RegularExpressions;

//namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
//{
//    public partial class ResetPassword : IDisposable
//    {
//        [Inject] protected NavigationManager Navigation { get; set; } = default!;

//        [Parameter] public string Email { get; set; } = string.Empty;

//        protected string NewPassword { get; set; } = "";
//        protected string ConfirmPassword { get; set; } = "";
//        protected string? ErrorMessage { get; set; }
//        protected string? ActiveErrorField { get; set; }
//        protected bool IsProcessing { get; set; }
//        protected string passwordType = "password";

//        private System.Timers.Timer? _timer;

//        // Note: OTP logic is removed here as per your "Best Approach" 
//        // because OTP is now verified on the VerifyOtp page.

//        protected async Task HandleReset()
//        {
//            ValidateWaterfall();

//            if (ActiveErrorField == null)
//            {
//                IsProcessing = true;
//                try
//                {
//                    await Task.Delay(1500); // Simulate API call
//                    Navigation.NavigateTo("/login");
//                }
//                catch (Exception)
//                {
//                    ErrorMessage = "Something went wrong. Please try again.";
//                }
//                finally
//                {
//                    IsProcessing = false;
//                }
//            }
//        }

//        private void ValidateWaterfall()
//        {
//            ActiveErrorField = null;
//            ErrorMessage = null;

//            if (string.IsNullOrEmpty(NewPassword)) { ActiveErrorField = "password"; ErrorMessage = "Password is required."; return; }
//            if (NewPassword.Length < 8) { ActiveErrorField = "password"; ErrorMessage = "At least 8 characters."; return; }
//            if (!NewPassword.Any(char.IsUpper)) { ActiveErrorField = "password"; ErrorMessage = "Need one Uppercase."; return; }
//            if (!NewPassword.Any(char.IsDigit)) { ActiveErrorField = "password"; ErrorMessage = "Need one Number."; return; }

//            if (ConfirmPassword != NewPassword) { ActiveErrorField = "confirm"; ErrorMessage = "Passwords do not match."; return; }
//        }

//        public void Dispose()
//        {
//            _timer?.Stop();
//            _timer?.Dispose();
//        }
//    }
//}

//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Web;
//using System.Text.RegularExpressions;

//namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
//{
//    public partial class ResetPassword : IDisposable
//    {
//        [Inject] protected NavigationManager Navigation { get; set; } = default!;

//        [Parameter] public string Email { get; set; } = string.Empty;

//        // Missing properties added here
//        protected string Otp { get; set; } = "";
//        protected string NewPassword { get; set; } = "";
//        protected string ConfirmPassword { get; set; } = "";
//        protected string? ErrorMessage { get; set; }
//        protected string? ActiveErrorField { get; set; }
//        protected bool IsProcessing { get; set; }
//        protected bool IsSuccess { get; set; }
//        protected string passwordType = "password";
//        protected string eyeIcon = "assets/user-management-assets/icons/monkey-password-hide.png";

//        // Timer properties
//        protected int Counter { get; set; } = 60;
//        protected string TimerText => $"00:{Counter:D2}";
//        protected bool IsTimerExpired => Counter <= 0;
//        private System.Timers.Timer? _timer;

//        protected override void OnInitialized() => StartTimer();

//        protected void HandleOtpInput(ChangeEventArgs e)
//        {
//            string rawValue = e.Value?.ToString() ?? "";
//            Otp = Regex.Replace(rawValue, @"[^0-9]", "");
//            ValidateWaterfall();
//        }

//        protected void StartTimer()
//        {
//            Counter = 60;
//            _timer?.Dispose();
//            _timer = new System.Timers.Timer(1000);
//            _timer.Elapsed += async (s, e) => {
//                if (Counter > 0) { Counter--; await InvokeAsync(StateHasChanged); }
//                else { _timer.Stop(); await InvokeAsync(StateHasChanged); }
//            };
//            _timer.Start();
//        }

//        protected void ResendOtp() { IsSuccess = false; StartTimer(); ActiveErrorField = null; }

//        protected void TogglePassword()
//        {
//            passwordType = (passwordType == "password") ? "text" : "password";
//            eyeIcon = (passwordType == "password")
//                ? "assets/user-management-assets/icons/monkey-password-hide.png"
//                : "assets/user-management-assets/icons/monkey-password-show.png";
//        }

//        protected async Task HandleReset()
//        {
//            ValidateWaterfall();
//            if (ActiveErrorField == null)
//            {
//                IsProcessing = true;
//                await Task.Delay(1500);
//                Navigation.NavigateTo("/login");
//                IsProcessing = false;
//            }
//        }

//        protected void ValidateWaterfall()
//        {
//            ActiveErrorField = null;
//            ErrorMessage = null;

//            if (string.IsNullOrEmpty(Otp)) { ActiveErrorField = "otp"; ErrorMessage = "OTP required."; return; }
//            if (string.IsNullOrEmpty(NewPassword)) { ActiveErrorField = "password"; ErrorMessage = "Password required."; return; }
//            if (NewPassword.Length < 8) { ActiveErrorField = "password"; ErrorMessage = "Min 8 chars."; return; }
//            if (!NewPassword.Any(char.IsUpper)) { ActiveErrorField = "password"; ErrorMessage = "Need Uppercase."; return; }
//            if (!NewPassword.Any(char.IsDigit)) { ActiveErrorField = "password"; ErrorMessage = "Need Number."; return; }
//            if (ConfirmPassword != NewPassword) { ActiveErrorField = "confirm"; ErrorMessage = "No match."; return; }
//        }

//        public void Dispose() { _timer?.Dispose(); }
//    }
//}
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Text.RegularExpressions;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class ResetPassword : IDisposable
    {
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        [Parameter] public string Email { get; set; } = string.Empty;

        protected string NewPassword { get; set; } = "";
        protected string ConfirmPassword { get; set; } = "";
        protected string? ErrorMessage { get; set; }
        protected string? ActiveErrorField { get; set; }
        protected bool IsProcessing { get; set; }
        protected string passwordType = "password";
        protected string eyeIcon = "assets/user-management-assets/icons/monkey-password-hide.png";

        protected async Task HandleReset()
        {
            ValidateWaterfall();

            if (ActiveErrorField == null)
            {
                IsProcessing = true;
                try
                {
                    await Task.Delay(1500); // Simulate API call
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

            if (string.IsNullOrEmpty(NewPassword)) { ActiveErrorField = "password"; ErrorMessage = "Password is required."; return; }
            if (NewPassword.Length < 8) { ActiveErrorField = "password"; ErrorMessage = "At least 8 characters."; return; }
            if (!NewPassword.Any(char.IsUpper)) { ActiveErrorField = "password"; ErrorMessage = "Need one Uppercase."; return; }
            if (!NewPassword.Any(char.IsDigit)) { ActiveErrorField = "password"; ErrorMessage = "Need one Number."; return; }

            if (ConfirmPassword != NewPassword) { ActiveErrorField = "confirm"; ErrorMessage = "Passwords do not match."; return; }
        }

        protected void TogglePassword()
        {
            passwordType = (passwordType == "password") ? "text" : "password";
            eyeIcon = (passwordType == "password")
                ? "assets/user-management-assets/icons/monkey-password-hide.png"
                : "assets/user-management-assets/icons/monkey-password-show.png";
        }

        public void Dispose()
        {
            // Timer cleanup removed as it's not needed here
        }
    }
}