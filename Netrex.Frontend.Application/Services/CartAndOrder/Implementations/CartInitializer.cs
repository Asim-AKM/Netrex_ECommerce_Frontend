using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;

namespace Netrex.Frontend.Application.Services.CartAndOrder.Implementations
{
    public static class CartInitializer
    {
        public static async Task SeedLocalStorageAsync(ICartItemManager cartManager)
        {
            var cart = await cartManager.GetCartAsync();

            // Agar cart already empty nahi hai to skip
            if (cart.Items.Any())
                return;

            // Add static test items
            cart.Items.Add(new CartItemState
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Men Denim Jacket",
                Price = 3500,
                Quantity = 1,
                ImageUrl = "https://picsum.photos/id/1050/100/120"
            });

            cart.Items.Add(new CartItemState
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Wireless Headphones",
                Price = 4200,
                Quantity = 2,
                ImageUrl = "https://picsum.photos/id/1060/100/120"
            });

            cart.Items.Add(new CartItemState
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Smart Watch",
                Price = 5500,
                Quantity = 1,
                ImageUrl = "https://picsum.photos/id/1070/100/120"
            });

            // Save to localStorage
            await cartManager.ClearCartAsync(); // clear first if needed
            foreach (var item in cart.Items)
            {
                await cartManager.AddToCartAsync(item);
            }
        }
    }
}
