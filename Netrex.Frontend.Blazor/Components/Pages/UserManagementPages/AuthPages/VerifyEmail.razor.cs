using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class VerifyEmail
    {
        // Fixing Service Warnings with = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected IJSRuntime JSRuntime { get; set; } = default!;

        // Fixing String Warnings with = "";
        protected string Email { get; set; } = "";
        protected string? ErrorMessage { get; set; } // Nullable because it starts null
        protected bool IsProcessing { get; set; }

        // Fixing 'VerifyAndRedirect' visibility
        protected async Task VerifyAndRedirect()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
                {
                    ErrorMessage = "Please enter a valid email address.";
                    return;
                }

                IsProcessing = true;
                await Task.Delay(1000);
                Navigation.NavigateTo($"/reset-password/{Email}");
            }
            catch (Exception) // Removed 'ex' to fix 'variable declared but never used'
            {
                ErrorMessage = "An error occurred.";
            }
            finally
            {
                IsProcessing = false;
            }
        }
    }
}