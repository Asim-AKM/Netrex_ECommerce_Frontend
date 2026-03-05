using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.ViewModels.WishList;

namespace Netrex.Frontend.Application.Services.WishList
{
    public interface IWishListManager
    {
        Task<ApiResponse<List<VmGetWishListItem>>> GetWishListItemsAsync(Guid userId);
        Task<ApiResponse<Guid>> AddWishListItemAsync(VmAddWishListItem request);
        Task<ApiResponse<string>> DeleteWishListItemAsync(Guid wishListItemId);
        Task<ApiResponse<int>> GetWishListCountAsync(Guid userId);
    }
}