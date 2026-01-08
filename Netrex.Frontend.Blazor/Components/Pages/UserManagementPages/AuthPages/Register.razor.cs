using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.UserManagement.Authentication;
using System.Net;

namespace Netrex.Frontend.Blazor.Components.Pages.UserManagementPages.AuthPages
{
    public partial class Register
    {
        [Inject]
        private IAuthManager _authManager { get; set; }
        VmRegister _model = new VmRegister();
        private string? message;
        public async Task HandleSignUp()
        {
            var response = await _authManager.RegisterAsync(_model);
            if (response == HttpStatusCode.Created)
            {
                message = "Created Succefuly";
            }
            else if (response == HttpStatusCode.BadGateway)
            {
                message = "Validation Error";
            }
            else
            {
                message = "Response is not valide(false)";
            }
        }
    }
}
