using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;
using System.Text.Json;
using Netrex.Frontend.Application.Services.Common;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class Register
    {

        [Inject]
        private IAuthManager _authManager { get; set; }
        [Inject]
        public ToastManager _toastManager { get; set; }
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
                _toastManager.Success("Registration successful! You can now log in.", "Registration Successful");
                generalMessage = response.Message; // "User Created Successfully"
                _model = new VmRegister();
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


    }
}
