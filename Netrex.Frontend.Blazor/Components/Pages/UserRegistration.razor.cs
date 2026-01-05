
using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Blazor.Models;
using Netrex.Frontend.Blazor.Services;

namespace Netrex.Frontend.Blazor.Components.Pages
{
    public partial class UserRegistration
    {
        [Inject]
        public IAuthService _authService { get; set; }
        VMUserRegistration _model = new VMUserRegistration();

        public void HandleSingUp()
        {
           var response =  _authService.UserRegisterAsync(_model);
        }
    }
}
