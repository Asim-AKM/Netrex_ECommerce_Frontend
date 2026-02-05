using Netrex.Frontend.Application.DTO_s;
using Netrex.Frontend.Application.Services.Customer.Interfaces;
using System.Net.Http.Json;

public class CustomerManager : ICustomerManager
{
    private readonly HttpClient _http;

    public CustomerManager(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<bool> UpdateCustomerAsync(UpdateCustomerDto customer)
    {
        // Aapka controller route "api/Customer/updateCustomer" hai
        var response = await _http.PutAsJsonAsync("api/Customer/updateCustomer", customer);
        return response.IsSuccessStatusCode;
    }
}