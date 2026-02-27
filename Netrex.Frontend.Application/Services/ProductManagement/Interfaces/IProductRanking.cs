using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.ViewModels.ProductManagement;

namespace Netrex.Frontend.Application.Services.ProductManagement.Interfaces
{
    public interface IProductRanking
    {
        Task<ApiResponse<List<ProductsVm>>> GetBestSellersAsync();
        Task<ApiResponse<List<ProductsVm>>> GetTrendingAsync();
        Task<ApiResponse<List<ProductsVm>>> GetTopRatedAsync();
        Task<ApiResponse<List<ProductsVm>>> GetHomepageProductsAsync();
        Task<ApiResponse<List<ProductsVm>>> GetNewArrivalsAsync();
    }
}
