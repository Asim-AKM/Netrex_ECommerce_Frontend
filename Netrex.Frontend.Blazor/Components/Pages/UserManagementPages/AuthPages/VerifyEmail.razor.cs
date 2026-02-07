using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.Common;
using System.ComponentModel.DataAnnotations;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class VerifyEmail : IDisposable
    {
        [Inject] public ToastService _toastService { get; set; } = default!;
        [Inject] public NavigationManager Navigation { get; set; } = default!;

        protected EmailModel Model { get; set; } = new();
        protected bool IsProcessing { get; set; }

        public async Task HandleRegister()
        {
            IsProcessing = true;
            try
            {
                await Task.Delay(100);

                _toastService.Success("OTP Sent Successfully!");
                Navigation.NavigateTo($"/verifyotp/{Uri.EscapeDataString(Model.Email)}");
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
        public class EmailModel
        {
            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
            public string Email { get; set; } = string.Empty;
        }
    }
}