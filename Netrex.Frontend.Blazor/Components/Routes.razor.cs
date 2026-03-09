using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace Netrex.Frontend.Blazor.Components
{
    public partial class Routes
    {
        [Inject] NavigationManager navigationManager { get; set; } = default!;
        public async Task HandleNotAuthorize(ClaimsPrincipal user)
        {
            if (user.Identity!.IsAuthenticated == false)
                navigationManager.NavigateTo("/login");
            else
                navigationManager.NavigateTo("/401");
        }
    }
}
