using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Netrex.Frontend.Application.Commons.SharedClasses;
using System.Net;
using System.Net.Http.Headers;

namespace Netrex.Frontend.Blazor.Middleware
{
    public class JwtAuthHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NavigationManager _navManager;
        private string? _cachedToken;
        private readonly object _lock = new object();

        public JwtAuthHandler(IHttpContextAccessor httpContextAccessor, NavigationManager navigationManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _navManager = navigationManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var endpoint = request.RequestUri?.PathAndQuery ?? "";

            if (!PublicEndpoints.IsPublic(endpoint))
            {
                var token = GetToken();

                Console.WriteLine("TOKEN: " + token);

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }
            }

            var response = await base.SendAsync(request, cancellationToken);

           

            return response;
        }

        private string? GetToken()
        {
            if (!string.IsNullOrEmpty(_cachedToken))
                return _cachedToken;

            lock (_lock)
            {
                if (!string.IsNullOrEmpty(_cachedToken))
                    return _cachedToken;

                _cachedToken = _httpContextAccessor
                    .HttpContext?
                    .User.FindFirst(ClaimKey.Jwt)?.Value;

                return _cachedToken;
            }
        }
    }
}