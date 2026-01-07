using Microsoft.Extensions.DependencyInjection;
using Netrex.Frontend.Application.Services.UserManagement.Implementations;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;

namespace Netrex.Frontend.Application.DIs
{
    public static class ApplicationDIs
    {
        public static IServiceCollection AddApplicationDIs(this IServiceCollection services)=>services
                                                                 .AddScoped<IAuthManager, AuthManager>();
    }
}
