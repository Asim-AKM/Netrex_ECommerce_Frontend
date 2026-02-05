using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Blazor.Services;
using System.Timers;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class PassswordResetOTP : IDisposable
    {
        [Parameter] public string Email { get; set; } = "";

        [Inject] public NavigationManager Navigation { get; set; } = default!;
        [Inject] public ToastService _toastService { get; set; } = default!;

        private string OtpCode { get; set; } = "";
        private bool IsProcessing { get; set; }

        // Timer Logic
        private int ResendTimer { get; set; } = 30;
        private bool IsResendDisabled { get; set; } = true;
        private System.Timers.Timer? _timer;

        protected override void OnInitialized()
        {
            StartTimer();
        }

        private void StartTimer()
        {
            IsResendDisabled = true;
            ResendTimer = 60;
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += (sender, e) =>
            {
                if (ResendTimer > 0)
                {
                    ResendTimer--;
                    InvokeAsync(StateHasChanged);
                }
                else
                {
                    IsResendDisabled = false;
                    _timer.Stop();
                    InvokeAsync(StateHasChanged);
                }
            };
            _timer.Start();
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
                // Yahan aap apna real API call lagayenge
                await Task.Delay(2000);

                _toastService.Success("Email verified successfully! Welcome to NETREX.");
                Navigation.NavigateTo("/reset-password/{Email}" + Email);
            }
            catch (Exception ex)
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
            _timer?.Dispose();
        }
    }
}