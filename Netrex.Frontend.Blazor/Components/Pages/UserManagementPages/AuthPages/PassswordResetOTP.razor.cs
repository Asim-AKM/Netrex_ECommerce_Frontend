using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Netrex.Frontend.Application.Services.Common;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class PassswordResetOTP : IDisposable
    {
        [Parameter] public string Email { get; set; } = "";

        [Inject] public NavigationManager Navigation { get; set; } = default!;
        [Inject] public ToastService _toastService { get; set; } = default!;

        private string _otpCode = "";
        private string OtpCode
        {
            get => _otpCode;
            set
            {
                // Sirf digits allow karein aur max 6 length rakhein
                if (!string.IsNullOrEmpty(value))
                {
                    _otpCode = new string(value.Where(char.IsDigit).Take(6).ToArray());
                }
                else
                {
                    _otpCode = "";
                }
            }
        }

        private bool IsProcessing { get; set; }

        // Timer Logic
        private int ResendTimer { get; set; } = 150;
        private bool IsResendDisabled { get; set; } = true;
        private System.Timers.Timer? _timer;

        protected override void OnInitialized()
        {
            StartTimer();
        }

        private void StartTimer()
        {
            IsResendDisabled = true;
            ResendTimer = 150;
            _timer?.Dispose();
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += async (sender, e) =>
            {
                if (ResendTimer > 0)
                {
                    ResendTimer--;
                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    IsResendDisabled = false;
                    _timer?.Stop();
                    await InvokeAsync(StateHasChanged);
                }
            };
            _timer.Start();
        }
        //this function will handle the key press event and only allow digits to be entered in the OTP input field
        private void HandleKeyPress(KeyboardEventArgs e)
        {
            
        }

        private async Task VerifyOtp()
        {
            if (string.IsNullOrEmpty(OtpCode) || OtpCode.Length != 6)
            {
                _toastService.Error("Please enter the complete 6-digit code.");
                return;
            }

            IsProcessing = true;
            try
            {
                // Simulation of API Call
                await Task.Delay(2000);

                _toastService.Success("Email verified successfully! Welcome to NETREX.");
                // Correct Navigation Path
                Navigation.NavigateTo($"/reset-password/{Uri.EscapeDataString(Email)}");
            }
            catch (Exception)
            {
                _toastService.Error("Invalid OTP. Please try again.");
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private void ResendOtp()
        {
            OtpCode = ""; // Clear existing
            _toastService.Info("A new code has been sent to " + Email);
            StartTimer();
            // TODO: Call your Resend API here
        }

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }
    }
}