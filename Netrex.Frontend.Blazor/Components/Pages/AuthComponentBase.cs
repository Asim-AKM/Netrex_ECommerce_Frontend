using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Netrex.Frontend.Application.Commons;
using System.Security.Claims;

namespace Netrex.Frontend.Blazor.Components
{
    public class AuthComponentBase : ComponentBase
    {
        [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
        [Inject] private NavigationManager NavManager { get; set; } = default!;

        protected Guid CurrentUserId { get; private set; }
        protected string CurrentUserName { get; private set; } = "";
        protected string CurrentUserRole { get; private set; } = "";
        protected bool IsAuthenticated { get; private set; }
        protected string ProfileImageUrl { get; private set; } = "";

        protected override async Task OnInitializedAsync()
        {
            var auth = await AuthStateProvider
                .GetAuthenticationStateAsync();
            var user = auth.User;

            IsAuthenticated = user.Identity?.IsAuthenticated ?? false;

            if (IsAuthenticated)
            {
                CurrentUserId = Guid.TryParse(
                    user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    out var guid) ? guid : Guid.Empty;

                CurrentUserName = user.Identity?.Name ?? "";

                CurrentUserRole = user
                    .FindFirst(ClaimTypes.Role)?.Value ?? "";
                ProfileImageUrl = user.FindFirst(ClaimKey.ProfileImageUrl)?.Value ?? "";
            }
        }

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

   
    }
}