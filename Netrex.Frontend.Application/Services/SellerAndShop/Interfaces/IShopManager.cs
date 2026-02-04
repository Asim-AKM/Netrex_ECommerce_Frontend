using Netrex.Frontend.Application.ViewModels.SellerModule;

namespace Netrex.Frontend.Application.Services.SellerAndShop.Interfaces
{
    public interface IShopManager
    {
        Task<List<VmShopDetail>> GetAllShopsAsync();
    }
}
