using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;
using System.Text.Json;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class Register
    {

        [Inject]
        private IAuthManager _authManager { get; set; }
        [Inject]
        public ToastService _toastManager { get; set; }

        [Inject]
        private NavigationManager _navigation { get; set; }

        private VerifyOTP otpModal = default!; // Reference to OTP modal

        VmRegister _model = new VmRegister();
        string? generalMessage;
        Dictionary<string, string> fieldErrors = new Dictionary<string, string>();

        public async Task HandleSignUp()
        {
            generalMessage = null;
            fieldErrors.Clear();

            var response = await _authManager.RegisterAsync<object>(_model);

            if (response.IsSuccess)
            {
                // Store email in session/local storage for OTP page
                await JSRuntime.InvokeVoidAsync("localStorage.setItem", "pendingVerificationEmail", _model.Email);

                _toastManager.Success(response.Message, "Registration Successful");

                otpModal.Show(_model.Email!);
            }

            else if (response.Status == 409) // Conflict - Email/Username exists
            {
                generalMessage = response.Message;

                // Check if message says "pending verification"
                if (response.Message.Contains("pending verification", StringComparison.OrdinalIgnoreCase) ||
                    response.Message.Contains("not verified", StringComparison.OrdinalIgnoreCase))
                {
                    // User clicked register again with pending email - OTP was resent
                    await JSRuntime.InvokeVoidAsync("localStorage.setItem", "pendingVerificationEmail", _model.Email);
                    _toastManager.Info(response.Message, "OTP Resent");
                    otpModal.Show(_model.Email!);
                }
                else
                {
                    _toastManager.Error(response.Message, "Registration Failed");
                }
            }

            else if (response.Status == 400 && response.Data is JsonElement element)
            {
                var errors = JsonSerializer.Deserialize<List<ValidationError>>(element.GetRawText());

                foreach (var error in errors!)
                {
                    fieldErrors[error.Field] = string.Join(", ", error.Errors);
                }

                generalMessage = response.Message;
            }
            else
            {
                generalMessage = response.Message;
            }
        }

        private void HandleOtpVerified()
        {
            // Called when OTP is successfully verified
            _toastManager.Success("Email verified! Redirecting to login...", "Success");

            // Navigate to login page
            _navigation.NavigateTo("/login");
        }

        // MISSING METHOD 2
        private void HandleModalClosed()
        {
            // Called when user closes modal without verifying
            // Optional: You can clear form or show a message
            // For now, just do nothing - user stays on register page
        }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }  //for localStorage
    }
}
