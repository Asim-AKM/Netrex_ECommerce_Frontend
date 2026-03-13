using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.ViewModels.SellerModule;

namespace Netrex.Frontend.Application.Services.SellerAndShop.Interfaces
{
    public interface IAdminSellerModule
    {
        // GET: all pending sellers
        Task<ApiResponse<List<VMGetSeller>>> GetPendingSellersAsync();

        // Approve seller
        Task<ApiResponse<bool>> ApproveSellerAsync(Guid sellerId);

        // Reject seller
        Task<ApiResponse<bool>> RejectSellerAsync(Guid sellerId);

        // Suspend seller
        Task<ApiResponse<bool>> SuspendSellerAsync(Guid sellerId);
    }
}
