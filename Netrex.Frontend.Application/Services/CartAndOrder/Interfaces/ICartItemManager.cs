using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Cart;

namespace Netrex.Frontend.Application.Services.CartAndOrder.Interfaces
{
    public  interface ICartItemManager
    {
        Task<ApiResponse<bool>> AddCartItemAsync(VmAddCartItem vm);
        Task<ApiResponse<List<VmGetCartItem>>>GetCartItemAsync();  
        Task<ApiResponse<bool>> IncreaseQuantityAsync(Guid cartitemid);
        Task<ApiResponse<bool>> DecreaseQuantityAsync(Guid cartItemId);
        Task<ApiResponse<bool>> RemoveItemAsync(Guid cartitemid);
    }
}
