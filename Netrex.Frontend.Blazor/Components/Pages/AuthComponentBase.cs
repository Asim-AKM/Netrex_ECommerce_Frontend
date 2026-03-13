using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Commons.SharedClasses;
using Netrex.Frontend.Application.Services.UserManagement.Implementations;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Blazor.Components.Layout;
using Netrex.Frontend.Blazor.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
namespace Netrex.Frontend.Blazor.Components
{
    public class AuthComponentBase : ComponentBase, IAsyncDisposable
    {
        [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
        [Inject] private NavigationManager NavManager { get; set; } = default!;
        [Inject] private IAuthManager AuthManager { get; set; } = default!;  // Add karo
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

        protected Guid CurrentUserId { get; private set; }
        protected string CurrentUserName { get; private set; } = "";
        protected string CurrentUserRole { get; private set; } = "";
        protected bool IsAuthenticated { get; private set; }
        protected string ProfileImageUrl { get; private set; } = "";
        private Timer? _refreshTimer;

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

                StartRefreshTimer(user);
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

        private void StartRefreshTimer(ClaimsPrincipal user)
        {
            var jwtToken = user.FindFirst(ClaimKey.Jwt)?.Value;
            if (string.IsNullOrEmpty(jwtToken)) return;

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(jwtToken);
            var expiry = jwt.ValidTo;

            var timeUntilRefresh = expiry - DateTime.UtcNow - TimeSpan.FromMinutes(2);

            if (timeUntilRefresh <= TimeSpan.Zero)
                timeUntilRefresh = TimeSpan.FromSeconds(30);

            _refreshTimer = new Timer(async _ =>
            {
                await RefreshTokenSilently();
            }, null, timeUntilRefresh, Timeout.InfiniteTimeSpan);
        }

        private async Task RefreshTokenSilently()
        {
            try
            {
                var result = await AuthManager.RefreshTokenAsync();
                if (result.IsSuccess && result.Data != null)
                {
                    await JSRuntime.InvokeVoidAsync("submitLoginForm", result.Data, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Token refresh fail: " + ex.Message);
            }
        }

        protected void HandleApiResponse<T>(ApiResponse<T> response)
        {
            switch (response.Status)
            {
                case (int)HttpStatusCode.Unauthorized:
                    NavManager.NavigateTo("/login", forceLoad: true);
                    break;
                case (int)HttpStatusCode.Forbidden:
                    NavManager.NavigateTo("/401", forceLoad: true);
                    break;
                case (int)HttpStatusCode.ServiceUnavailable:
                    break;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_refreshTimer != null)
                await _refreshTimer.DisposeAsync();
        }


    }
}