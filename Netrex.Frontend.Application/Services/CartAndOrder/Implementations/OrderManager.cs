using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.CartAndOrder.Interfaces;
using Netrex.Frontend.Application.ViewModels.CartAndOrderModule.Order;
using Netrex.Frontend.Blazor.Services;

namespace Netrex.Frontend.Application.Services.CartAndOrder.Implementations
{
    public class OrderManager : IOrderManager
    {
        private readonly HttpClient _httpClient;
        private readonly LoaderService _loader;

        public OrderManager(IHttpClientFactory httpClientFactory, LoaderService loader)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _loader = loader;
        }



        public async Task<ApiResponse<List<VmGetOrder>>> vmGetOrders()
        {
            try
            {
                _loader.Show();

                var response = await _httpClient.GetAsync("/api/v1/Order");
                var json = await response.Content.ReadAsStringAsync();

                return ApiResponseDeserializer.Deserialize<List<VmGetOrder>>(json);
            }
            finally
            {
                _loader.Hide();
            }
        }
    }
}
