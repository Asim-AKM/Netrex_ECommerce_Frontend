using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.Services.Storage.Interface;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;
namespace Netrex.Frontend.Application.Services.CartAndOrder.Implementations
{
    public class CartItemManager(ILocalStorageManager localStorage) : ICartItemManager
    {
        private const string key = "cart_state";
        public async Task AddToCartAsync(CartItemState vm)
        {
            var cart = await GetCartAsync();
            var item= cart.Items.FirstOrDefault(i => i.ProductId == vm.ProductId);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Items.Add(vm);
            }
            await localStorage.SetAsync(key, cart);
        }

        public async Task<CartState> GetCartAsync()
        {
            return await localStorage.GetAsync<CartState>(key) ?? new CartState { Items = new List<CartItemState>() };
        }
        public async Task IncreaseQuantityAsync(Guid productId)
        {
            var cart = await GetCartAsync();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity++;
                await localStorage.SetAsync(key, cart);
            }
        }
        public async Task DecreaseQuantityAsync(Guid productId)
        {
            var cart = await GetCartAsync();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null && item.Quantity > 1)
            {
               item.Quantity--;
               await localStorage.SetAsync(key, cart);
            }
        }
        public async Task RemoveItemAsync(Guid productId)
        {
            var cart=await GetCartAsync();
            cart.Items.RemoveAll(i => i.ProductId == productId);
            await localStorage.SetAsync(key, cart);
        }
        public async Task ClearCartAsync()
        {
            await localStorage.RemoveAsync(key);
        }
    }
}
