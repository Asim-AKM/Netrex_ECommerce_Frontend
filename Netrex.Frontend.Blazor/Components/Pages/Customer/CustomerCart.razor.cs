using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;
using Netrex.Frontend.Application.Services.Common;

namespace Netrex.Frontend.Blazor.Components.Pages.Customer
{
    public partial class CustomerCart
    {
        [Inject]
        private ICartItemManager CartItemManager { get; set; } = default!;

        [Inject]
        private ToastService _Toast { get; set; } = default!;
        [Inject] NavigationManager navigationManager { get; set; }

        private List<VmGetCartItem> getcartItems = new();

        private bool IsLoading = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadCartAsync();
        }

        private async Task LoadCartAsync()
        {
            try
            {
                IsLoading = true;

                var response = await CartItemManager.GetCartItemAsync();

                if (!response.IsSuccess)
                {
                    _Toast.Error(response.Message ?? "Failed to load cart items");
                    return;
                }

                getcartItems = response.Data ?? new List<VmGetCartItem>();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task RemoveItem(Guid cartItemId)
        {
            var response = await CartItemManager.RemoveItemAsync(cartItemId);

            if (!response.IsSuccess)
            {
                _Toast.Error("Failed to remove item");
                return;
            }

            _Toast.Success("Item removed");

            await LoadCartAsync();
        }
        public void CheckOut()
        {
            navigationManager.NavigateTo("/cart");
        }
    }
}