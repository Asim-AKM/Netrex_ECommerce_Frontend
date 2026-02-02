using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.CartAndOrder.Implementations;
using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;

namespace Netrex.Frontend.Blazor.Components.Pages.CartAndOrderPages.CartPages
{
    public partial class Cart
    {
        [Inject] private ICartItemManager CartItemManager { get; set; } = default!;
        [Inject] private ToastService _Toast { get; set; } = default!;
        [Inject] private NavigationManager navigationManager { get; set; } = default!;

        private CartState cartState = new();

        private decimal Subtotal => (decimal)cartState.TotalAmount;
        private decimal Shipping { get; set; } = 500;
        private decimal Total => Subtotal + Shipping;
        protected override async Task OnInitializedAsync()
        {
            // Seed local storage with static cart items
            await CartInitializer.SeedLocalStorageAsync(CartItemManager);
            await LoadCartAsync();
        }
        private async Task LoadCartAsync()
        {
            cartState = await CartItemManager.GetCartAsync();
            StateHasChanged();
        }
        private async Task IncreaseQuantity(CartItemState item)
        {
            await CartItemManager.IncreaseQuantityAsync(item.ProductId);
            await LoadCartAsync();
        }

        private async Task DecreaseQuantity(CartItemState item)
        {
            
            if (item.Quantity >= 1)
            {
                await CartItemManager.DecreaseQuantityAsync(item.ProductId);
            }
            else
            {
                _Toast.Error("Failed to decrease quantity");
            }
            await LoadCartAsync();
        }

        private async Task RemoveItem(CartItemState item)
        {
            await CartItemManager.RemoveItemAsync(item.ProductId);
            await LoadCartAsync();
        }

        private void ProceedToCheckOut()
        {
            if (!cartState.Items.Any())
            {
                _Toast.Error("Your cart is empty!");
                return;
            }
            navigationManager.NavigateTo("/DeliveryAddressPage");
        }

    }
}
