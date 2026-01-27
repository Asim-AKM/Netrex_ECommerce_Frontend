using Netrex.Frontend.Application.ViewModels.ProductManagement;

namespace Netrex.Frontend.Application.Services.ProductManagement.Interfaces
{
    public interface IProductManager
    {
        Task<ProductsVm> AddProducts( ProductsVm productsVm);
        Task<bool> RemoveProducts(int productId);
        Task<ProductsVm> UpdateProducts(ProductsVm productsVm);
        Task<ProductsVm> GetProductByIdAsync(int productId);
        Task<IEnumerable<ProductsVm>> GetAllProductsAsync();
    }
}
