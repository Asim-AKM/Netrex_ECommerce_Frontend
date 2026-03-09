using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Netrex.Frontend.Application.Commons.AppResponses;
using System.Net.Http.Json;

namespace Netrex.Frontend.Blazor.Middleware
{
    public class JwtRefreshMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(
            HttpContext context,
            IHttpClientFactory httpClientFactory)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var token = context.User.FindFirst("jwt")?.Value;

                if (!string.IsNullOrEmpty(token))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwt = handler.ReadJwtToken(token);

                    if (jwt.ValidTo < DateTime.UtcNow.AddMinutes(5))
                    {
                        var userIdClaim = context.User
                            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                        if (Guid.TryParse(userIdClaim, out var userId))
                        {
                            await RefreshJwtSilently(
                                context, httpClientFactory, userId);
                        }
                    }
                }
            }

            await _next(context);
        }

        private async Task RefreshJwtSilently(
            HttpContext context,
            IHttpClientFactory factory,
            Guid userId)
        {
            try
            {
                var client = factory.CreateClient("ApiClient");
                var response = await client.GetAsync(
                    $"UserSession/GetRefreshJwt?userId={userId}");

                if (!response.IsSuccessStatusCode) return;

                var result = await response.Content
                    .ReadFromJsonAsync<ApiResponse<string>>();

                if (result == null || !result.IsSuccess) return;

                var handler = new JwtSecurityTokenHandler();
                var newJwt = handler.ReadJwtToken(result.Data);

                var claims = newJwt.Claims.ToList();
                claims.Add(new Claim("jwt", result.Data));

                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name,
                    ClaimTypes.Role
                );

                var existingAuth = await context.AuthenticateAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);

                await context.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    existingAuth.Properties!
                );

                context.User = new ClaimsPrincipal(identity);
            }
            catch
            {
            }
        }
    }
}