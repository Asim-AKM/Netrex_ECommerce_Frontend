using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Services.SellerAndShop.Interfaces;
using Netrex.Frontend.Application.ViewModels.SellerModule;

namespace Netrex.Frontend.Application.Services.SellerAndShop.Implementations
{
    public class ShopManager : IShopManager
    {
        private readonly HttpClient _httpClient;
        public ShopManager(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        public async Task<List<VmShopDetail>> GetAllShopsAsync()
        {
             var response = await _httpClient.GetAsync("api/v1/ShopDetails/GetAllShopDetails");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to retrieve ShopCatagory.");
            }
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = ApiResponseDeserializer.Deserialize<List<VmShopDetail>>(jsonString);

            if (apiResponse != null && apiResponse.IsSuccess)
            {
                return apiResponse.Data ?? new List<VmShopDetail>();
            }

            return new List<VmShopDetail>();

        }
    }
}
