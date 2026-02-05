using Netrex.Frontend.Application.DTO_s;
using System.Net.Http.Json;

public class CustomerManager
{
    private readonly HttpClient _http;

    public CustomerManager(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> UpdateCustomerAsync(UpdateCustomerDto customer)
    {
        // Aapka controller route "api/Customer/updateCustomer" hai
        var response = await _http.PutAsJsonAsync("api/Customer/updateCustomer", customer);
        return response.IsSuccessStatusCode;
    }
}