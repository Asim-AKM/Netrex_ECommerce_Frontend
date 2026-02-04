//using Microsoft.AspNetCore.Components;
//using System.Threading.Tasks;

//namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
//{
//    public partial class VerifyEmail
//    {
//        // Yahan [Inject] ki zaroorat nahi thi, ye simple string honi chahiye
//        private string Email { get; set; } = "";
//        private string? ErrorMessage { get; set; }
//        private bool IsProcessing { get; set; }

//        private async Task VerifyAndRedirect()
//        {
//            ErrorMessage = null;

//            // Basic Email Validation
//            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
//            {
//                ErrorMessage = "Please enter a valid email address.";
//                return;
//            }

//            IsProcessing = true;

//            try
//            {
//                // Yahan aap apna API call kar saktay hain
//                await Task.Delay(1500);

//                // Redirecting to Reset Password page
//                Navigation.NavigateTo("/reset-password");
//            }
//            catch (System.Exception ex)
//            {
//                ErrorMessage = "An error occurred. Please try again.";
//            }
//            finally
//            {
//                IsProcessing = false;
//            }
//        }
//    }
//}