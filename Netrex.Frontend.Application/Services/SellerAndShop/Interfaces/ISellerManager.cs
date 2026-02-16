using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.ViewModels.SellerModule;

namespace Netrex.Frontend.Application.Services.SellerAndShop.Interfaces
{
    public interface ISellerManager
    {
        Task<ApiResponse<VmSeller>> CreateSellerAsync(VmSeller vmSeller);
        Task<ApiResponse<VmSeller>> UpdateSellerAsync(VmSeller vmSeller);
        Task<ApiResponse<string>> DeleteSellerAsync(Guid Id);
        Task<ApiResponse<List<VmSeller>>> GetSellerAsync();
        Task<ApiResponse<VmSeller>> GetSellerbyIdAsync(Guid Id);

    }
}
