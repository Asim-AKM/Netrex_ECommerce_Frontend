using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Blazor.Services;
using System.ComponentModel.DataAnnotations;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class ResetPassword
    {
        [Parameter] public string Email { get; set; } = string.Empty;

        [Inject] public LoaderService _loader { get; set; } = default!;
        [Inject] public ToastService _toastService { get; set; } = default!;
        [Inject] public NavigationManager _navigation { get; set; } = default!;

        private VmResetPassword _model = new VmResetPassword();
        private bool IsProcessing = false;

        // Toggle Password States
        private bool showNewPass = false;
        private bool showConfirmPass = false;

        public async Task HandleReset()
        {
            try
            {
                IsProcessing = true;
                _loader.Show();
                await Task.Delay(2000); // Simulation
                _toastService.Success("Password updated successfully!", "Success");
                _navigation.NavigateTo("/login");
            }
            catch (Exception ex)
            {
                _toastService.Error("Failed to reset password: " + ex.Message, "Error");
            }
            finally
            {
                IsProcessing = false;
                _loader.Hide();
            }
        }

        public class VmResetPassword : IValidatableObject
        {
            [Required(ErrorMessage = "New password is required")]
            public string NewPassword { get; set; } = string.Empty;

            [Required(ErrorMessage = "Please confirm your password")]
            public string ConfirmPassword { get; set; } = string.Empty;

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (string.IsNullOrWhiteSpace(NewPassword)) yield break;

                // Waterfall Validation - One by One
                if (NewPassword.Length < 8)
                {
                    yield return new ValidationResult("Minimum 8 characters required.", new[] { nameof(NewPassword) });
                }
                else if (!NewPassword.Any(char.IsUpper))
                {
                    yield return new ValidationResult("Need at least one uppercase (A-Z).", new[] { nameof(NewPassword) });
                }
                else if (!NewPassword.Any(char.IsLower))
                {
                    yield return new ValidationResult("Need at least one lowercase (a-z).", new[] { nameof(NewPassword) });
                }
                else if (!NewPassword.Any(char.IsDigit))
                {
                    yield return new ValidationResult("Need at least one digit (0-9).", new[] { nameof(NewPassword) });
                }
                else if (!NewPassword.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    yield return new ValidationResult("Need one special character (@, #, $).", new[] { nameof(NewPassword) });
                }

                if (!string.IsNullOrWhiteSpace(ConfirmPassword) && NewPassword != ConfirmPassword)
                {
                    yield return new ValidationResult("Passwords do not match.", new[] { nameof(ConfirmPassword) });
                }
            }
        }
    }
}