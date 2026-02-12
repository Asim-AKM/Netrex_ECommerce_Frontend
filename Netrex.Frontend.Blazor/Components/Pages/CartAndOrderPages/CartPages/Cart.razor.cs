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
        private bool IsCartEmpty = false;
        private bool IsLoading = false;


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
        protected override async Task OnInitializedAsync()
        {
            await LoadCartAsync();
        }
        public async Task LoadCartAsync()
        {
            try
            {
                IsLoading = true;
                IsCartEmpty = false;

                var response = await CartItemManager.GetCartItemAsync();
                if (!response.IsSuccess)
                {
                    _Toast.Error(response.Message ?? "Failed to load cart items");
                    return;
                }
                if (response.Data == null || !response.Data.Any())
                {
                    IsCartEmpty = true;
                    return;
                }
                getcartItems = response.Data;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task IncreaseQuantity(Guid itemId)
        {
            var response = await CartItemManager.IncreaseQuantityAsync(itemId);
            if (!response.IsSuccess)
            {
                _Toast.Error("Failed to increase quantity");
                return;
            }
            await LoadCartAsync();
        }

        private async Task DecreaseQuantity(Guid itemId)
        {
            var item=getcartItems.FirstOrDefault(i=>i.CartItemId==itemId);
            if(item==null)
            {
                return;
            }
            if(item.Quantity<=1)
            {
                _Toast.Warning("Quantity cannot be less than 1");
                return;
            }

            var response = await CartItemManager.DecreaseQuantityAsync(itemId);
            if (!response.IsSuccess)
            {
                _Toast.Error("Failed to decrease quantity");
                return;
            }
            await LoadCartAsync();

        }

        private async Task RemoveItem(Guid Itemid)
        {
            var response = await CartItemManager.RemoveItemAsync(Itemid);
            if (response.IsSuccess)
            {
                _Toast.Success("Item removed");
                await LoadCartAsync();
            }
     
        }

        private async Task ProceedToCheckOut()
        {
            navigationManager.NavigateTo("/DeliveryAddressPage");
        }



    }
}
