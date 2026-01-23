using Microsoft.AspNetCore.Components;
using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.Services.Common;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;

namespace Netrex.Frontend.Blazor.Components.Pages
{
    public partial class Home
    {
        [Inject]
        private ICartItemManager CartItemManager { get; set; } = default!;
        [Inject]
        private ToastManager _Toast { get; set; } = default!;
        VmAddCartItem model = new VmAddCartItem();
        public async Task AddToCart()
        {

            var response = await CartItemManager.AddCartItemAsync(model);

            if (!response.IsSuccess)
            {
                _Toast.Error(response.Message);
            }
        }
    }
}
