using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;

namespace Netrex.Frontend.Application.Services.CartAndOrder.Interfaces
{
    public  interface ICartItemManager
    {
        Task<CartState> GetCartAsync();
        Task AddToCartAsync(CartItemState vm);
        Task IncreaseQuantityAsync(Guid productId);
        Task DecreaseQuantityAsync(Guid productId);
        Task RemoveItemAsync(Guid productId);
        Task ClearCartAsync();
    }
}
