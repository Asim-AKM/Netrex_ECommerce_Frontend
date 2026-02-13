using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.ViewModels.ProductManagement;

namespace Netrex.Frontend.Application.Services.ProductManagement.Interfaces
{
    public interface IProductManager
    {

        Task<ApiResponse<ProductsVm>> AddProducts(ProductsVm productsVm,List<byte[]> imageBytes, List<string> imageNames);
        Task<ApiResponse<bool>> RemoveProducts(Guid productId);

        Task<ApiResponse<string>> UpdateProducts(ProductsVm productsVm);

        Task<ApiResponse<ProductsVm>> GetProductByIdAsync(Guid productId);

        Task<ApiResponse<List<ProductsVm>>> GetAllProductsAsync();

    }
}
