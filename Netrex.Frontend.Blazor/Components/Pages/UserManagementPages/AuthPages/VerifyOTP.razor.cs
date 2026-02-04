using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;
using System.Timers;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class VerifyOTP : IDisposable
    {
        [Inject]
        private IAuthManager _authManager { get; set; } = default!;

        [Inject]
        private NavigationManager _navigation { get; set; } = default!;

        [Inject]
        private ToastService _toastManager { get; set; } = default!;

        [Parameter]
        public EventCallback OnVerificationSuccess { get; set; }

        [Parameter]
        public EventCallback OnModalClosed { get; set; }

        public bool IsVisible { get; private set; } = false;

        private VmVerifyOtp _model = new VmVerifyOtp();
        private string? email;
        private string? generalMessage;
        private bool isSuccess = false;

        // Timer for resend OTP cooldown
        private bool canResend = false;
        private int remainingSeconds = 300; // 5 minutes
        private System.Timers.Timer? countdownTimer;

        // PUBLIC METHOD to show modal
        public void Show(string userEmail)
        {
            email = userEmail;
            _model.Email = userEmail;
            IsVisible = true;
            generalMessage = null;
            isSuccess = false;

            StartResendTimer();
            StateHasChanged();
        }

        // PUBLIC METHOD to hide modal
        public void Hide()
        {
            IsVisible = false;
            countdownTimer?.Stop();
            StateHasChanged();
        }

        private void CloseModal()
        {
            Hide();
            OnModalClosed.InvokeAsync();
        }

        private void StartResendTimer()
        {
            remainingSeconds = 300; // Reset to 5 minutes
            canResend = false;

            countdownTimer?.Stop();
            countdownTimer = new System.Timers.Timer(1000); // 1 second interval
            countdownTimer.Elapsed += OnTimerElapsed;
            countdownTimer.Start();
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            remainingSeconds--;

            if (remainingSeconds <= 0)
            {
                canResend = true;
                countdownTimer?.Stop();
            }

            InvokeAsync(StateHasChanged);
        }

        private string FormatTime(int seconds)
        {
            int minutes = seconds / 60;
            int secs = seconds % 60;
            return $"{minutes}:{secs:D2}";
        }

        private async Task HandleVerifyOtp()
        {
            generalMessage = null;
            isSuccess = false;

            var response = await _authManager.VerifyOtpAsync<object>(_model);

            if (response.IsSuccess)
            {
                isSuccess = true;
                generalMessage = response.Message;
                _toastManager.Success(response.Message, "Success");

                // Wait 1 second then close modal
                await Task.Delay(1000);
                Hide();

                // Notify parent component
                await OnVerificationSuccess.InvokeAsync();
            }
            else
            {
                generalMessage = response.Message;
                _toastManager.Error(response.Message, "Verification Failed");
            }
        }

        private async Task HandleResendOtp()
        {
            if (!canResend) return;

            generalMessage = null;

            var response = await _authManager.ResendOtpAsync<object>(email!);

            if (response.IsSuccess)
            {
                _toastManager.Success(response.Message, "OTP Resent");
                generalMessage = response.Message;

                // Restart timer
                StartResendTimer();
            }
            else
            {
                _toastManager.Error(response.Message, "Failed");
                generalMessage = response.Message;
            }
        }

        public void Dispose()
        {
            countdownTimer?.Stop();
            countdownTimer?.Dispose();
        }
    }
}