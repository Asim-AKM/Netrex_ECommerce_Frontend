using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Netrex.Frontend.Blazor.Components
{
    public class AuthComponentBase : ComponentBase
    {

        [Inject] protected AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
        [Inject] protected NavigationManager NavManager { get; set; } = default!;

        protected async Task<bool> IsLoggedIn()
        {
            var auth = await AuthStateProvider.GetAuthenticationStateAsync();
            return auth.User.Identity?.IsAuthenticated ?? false;
        }

        protected async Task<bool> EnsureLoggedIn()
        {
            if (!await IsLoggedIn())
            {
                NavManager.NavigateTo("/login", forceLoad: true);
                return false;
            }
            return true;
        }

        protected async Task<bool> RequireRole(string role)
        {
            var auth = await AuthStateProvider.GetAuthenticationStateAsync();
            var user = auth.User;

            if (!user.Identity!.IsAuthenticated)
            {
                NavManager.NavigateTo("/login", forceLoad: true);
                return false;
            }

            if (!user.IsInRole(role))
            {
                NavManager.NavigateTo("/401", forceLoad: true);
                return false;
            }

            return true;
        }

        protected async Task<ClaimsPrincipal> GetCurrentUser()
        {
            var auth = await AuthStateProvider.GetAuthenticationStateAsync();
            return auth.User;
        }

        protected async Task<Guid?> GetCurrentUserId()
        {
            var user = await GetCurrentUser();
            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(id, out var guid)
                ? guid : null;
        }

        protected async Task<string> GetCurrentUserName()
        {
            var user = await GetCurrentUser();
            return user.Identity?.Name ?? "";
        }

        protected async Task<string> GetCurrentUserRole()
        {
            var user = await GetCurrentUser();
            return user.FindFirst(ClaimTypes.Role)?.Value ?? "";
        }
    }
}