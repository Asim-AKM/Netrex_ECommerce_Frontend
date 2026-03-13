using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.SellerAndShop.Interfaces;
using Netrex.Frontend.Application.ViewModels.SellerModule;

namespace Netrex.Frontend.Application.Services.SellerAndShop.Implementations
{
    public class AdminSellerModule : IAdminSellerModule
    {
        private readonly HttpClient _httpClient;
        public AdminSellerModule(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        public Task<ApiResponse<bool>> ApproveSellerAsync(Guid sellerId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<VMGetSeller>>> GetPendingSellersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> RejectSellerAsync(Guid sellerId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> SuspendSellerAsync(Guid sellerId)
        {
            throw new NotImplementedException();
        }
    }
}
