using Netrex.Frontend.Application.Services.SellerAndShop.Interfaces;
using Netrex.Frontend.Application.ViewModels.SellerModule;
using Netrex.Frontend.Blazor.Services;
using System.Net.Http.Json;

namespace Netrex.Frontend.Application.Services.SellerAndShop.Implementations
{
    public class SellerManager : ISellerManager
    {
        private readonly HttpClient _httpClient;
        private readonly LoaderService _loader;
        public SellerManager(IHttpClientFactory httpClient, LoaderService loaderService)
        {
            _httpClient = httpClient.CreateClient("ApiClient");
            _loader = loaderService;
        }
        public async Task<VmSeller> CreateSellerAsync(VmSeller vmSeller)
        {
            try
            {
                _loader.Show();
                var response = await _httpClient.PostAsJsonAsync("api/Seller/CreateSeller", vmSeller);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<VmSeller>();
                    return json!;
                }
                else
                {
                    throw new Exception("Failed to add seller.");
                }
            }
            finally
            {
                _loader.Hide();
            }
        }
        public async Task<string> DeleteSellerAsync(Guid Id)
        {
            try
            {
                _loader.Show();
                var response = await _httpClient.DeleteAsync($"api/Seller/DeleteSeller/{Id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to delete Seller.");
                }
                return "Deleted Sucessfully";
            }
            finally
            {
                _loader.Hide();
            }
        }

        public async Task<VmSeller> GetSellerbyIdAsync(Guid Id)
        {
            try
            {
                _loader.Show();
                var response = await _httpClient.GetAsync($"api/Seller/GetSellerById{Id}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to retrieve Seller Data.");
                }
                var result = await response.Content.ReadFromJsonAsync<VmSeller>();
                return result ?? new VmSeller();
            }
            finally
            {
                _loader.Hide();
            }
        }

        public async Task<List<VmSeller>> GetSellerAsync()
        {
            try
            {
                _loader.Show();

                var response = await _httpClient.GetAsync(
                    "api/Seller/GetAllSellers");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to retrieve Seller.");
                }

                var products = await response.Content
                    .ReadFromJsonAsync<List<VmSeller>>();

                return products ?? new List<VmSeller>();
            }
            finally
            {
                _loader.Hide();
            }
        }

        public async Task<string> UpdateSellerAsync(VmSeller vmSeller)
        {
            try
            {
                _loader.Show();

                var response = await _httpClient.PutAsJsonAsync(
                    $"api/Product/UpdateSeller/{vmSeller.SellerId}", vmSeller);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to update Seller.");
                }

                var jason = await response.Content
                    .ReadFromJsonAsync<VmSeller>();

                return "Data successfully";
            }
            finally
            {
                _loader.Hide();
            }
        }
    }
}
