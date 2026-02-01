using Microsoft.Extensions.DependencyInjection;
using Netrex.Frontend.Application.Services.CartAndOrder.Implementations;
using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Application.Services.ProductManagement.Implementations;
using Netrex.Frontend.Application.Services.ProductManagement.Interfaces;
using Netrex.Frontend.Application.Services.UserManagement.Implementations;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;

namespace Netrex.Frontend.Application.DIs
{
    public static class ApplicationDIs
    {
        public static IServiceCollection AddApplicationDIs(this IServiceCollection services) => services
                                                                 .AddScoped<IAuthManager, AuthManager>()
                                                                 .AddScoped<ToastService>()
                                                                 .AddScoped<ICartItemManager, CartItemManager>()
                                                                 .AddScoped<IProductManager, ProductManager>()
                                                                 .AddScoped<ICloudnaryManager, CloudnaryManager>();
    }
}
