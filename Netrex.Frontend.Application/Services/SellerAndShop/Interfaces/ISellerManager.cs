using Netrex.Frontend.Application.ViewModels.SellerModule;

namespace Netrex.Frontend.Application.Services.SellerAndShop.Interfaces
{
    public interface ISellerManager
    {
        Task<string> CreateSellerAsync(VmSeller vmSeller);
        Task<string> UpdateSellerAsync(VmSeller vmSeller);
        Task<string> DeleteSellerAsync(Guid Id);
        Task<List<VmSeller>> GetSellerAsync();
        Task<VmSeller> GetSellerbyIdAsync(Guid Id);

    }
}
