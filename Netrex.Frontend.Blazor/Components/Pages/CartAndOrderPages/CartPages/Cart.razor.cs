using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;

namespace Netrex.Frontend.Blazor.Components.Pages.CartAndOrderPages.CartPages
{
    public partial class Cart
    {
        [Inject]
        private ICartItemManager CartItemManager { get; set; } = default!;
        [Inject]
        private ToastService _Toast { get; set; } = default!;

        private List<VmGetCartItem> getcartItems = new();

        // Calculated properties for the order summary
        private decimal Subtotal => (decimal)getcartItems.Sum(item => item.Price * item.Quantity);
        private decimal Shipping { get; set; } = 500;
        private decimal Total => Subtotal + Shipping;

        /// <summary>
        /// Represents a single item in the shopping cart.
        /// </summary>

        /// <summary>
        /// Initializes the component with sample data when it's first rendered.
        /// In a real application, this data would come from a service or API.
        /// </summary>
        protected override void OnInitialized()
        {
            GetCartItems();
        }
        public void GetCartItems()
        {
            getcartItems = new List<VmGetCartItem>
        {
            new VmGetCartItem
            {
                CartItemId = Guid.NewGuid(),
                ProductName = "Men Denim Jacket",
                Description = "Size: M",

                Price = 3500,
                Quantity = 1
            },
            new VmGetCartItem
            {
                CartItemId = Guid.NewGuid(),
                ProductName = "Wireless Headphones",
                Description = "Color: Black",

                Price = 4200,
                Quantity = 2
            },
            new VmGetCartItem
            {
                CartItemId = Guid.NewGuid(),
                ProductName = "Smart Watch",
                Description = "Color: Silver",

                Price = 5500,
                Quantity = 1
            }
        };
        }

        private async Task IncreaseQuantity(VmGetCartItem item)
        {
            var response = await CartItemManager.IncreaseQuantityAsync(item.CartItemId);
            if (response.IsSuccess)
            {
                GetCartItems();
            }
            else
            {
                _Toast.Error("Failed to increase quantity");
            }
        }

        private async Task DecreaseQuantity(VmGetCartItem item)
        {
            var response = await CartItemManager.DecreaseQuantityAsync(item.CartItemId);
            if (response.IsSuccess && item.Quantity > 1)
            {
                GetCartItems();
            }
            else
            {
                _Toast.Error("Failed to decrease quantity");
            }
        }

        private async Task RemoveItem(VmGetCartItem item)
        {
            var response = await CartItemManager.RemoveItemAsync(item.CartItemId);
            if (response.IsSuccess)
            {
                getcartItems.Remove(item);
                _Toast.Success("Item removed");
            }
            else
            {
                _Toast.Error("Failed to remove item");
            }
        }
    }
}
