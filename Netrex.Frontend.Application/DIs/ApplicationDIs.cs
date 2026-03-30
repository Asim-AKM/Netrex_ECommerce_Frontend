using Microsoft.Extensions.DependencyInjection;
using Netrex.Frontend.Application.Services.CartAndOrder.Implementations;
using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.Services.ChatBot.Implementations;
using Netrex.Frontend.Application.Services.ChatBot.Interfaces;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Application.Services.ProductManagement.Implementations;
using Netrex.Frontend.Application.Services.ProductManagement.Interfaces;
using Netrex.Frontend.Application.Services.SellerAndShop.Implementations;
using Netrex.Frontend.Application.Services.SellerAndShop.Interfaces;
using Netrex.Frontend.Application.Services.UserManagement.Implementations;
using Netrex.Frontend.Application.Services.UserManagement.Interfaces;
using Netrex.Frontend.Application.Services.WishList;

namespace Netrex.Frontend.Application.DIs
{
    public static class ApplicationDIs
    {
        public static IServiceCollection AddApplicationDIs(this IServiceCollection services) => services
                                                                 .AddScoped<IAuthManager, AuthManager>()
                                                                 .AddScoped<ToastService>()
                                                                 .AddScoped<ICartItemManager, CartItemManager>()
                                                                 .AddScoped<IProductManager, ProductManager>()
                                                                 .AddScoped<ICloudnaryManager, CloudnaryManager>()
                                                                .AddScoped<IShopManager, ShopManager>()
                                                                .AddScoped<ISellerManager, SellerManager>()
                                                                .AddScoped<IUserManager, UserManager>()
                                                                .AddScoped<IWishListManager, WishListManager>()
                                                                .AddScoped<WishListStateService>()
                                                                .AddScoped<IProductRanking, ProductRanking>()
                                                                 .AddScoped<IOrderManager, OrderManager>()
                                                                 .AddScoped<IChatBotManager, ChatBotManager>();
    }
}
