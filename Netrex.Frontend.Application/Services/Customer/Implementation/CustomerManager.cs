using Netrex.Frontend.Application.DTO_s;
using Netrex.Frontend.Application.Services.Customer.Interfaces;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace Netrex.Frontend.Application.Services.Customer.Implementation
{
    // Primary Constructor use ho raha hai
    public class CustomerManager(HttpClient http) : ICustomerManager
    {
        public async Task UpdateCustomerAsync(UpdateCustomerDto customer)
        {
            var response = await http.PutAsJsonAsync("api/Customer/updateCustomer", customer);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to update customer");
            }
        }
        public async Task UpdateProfileImageAsync(Guid userId, byte[] imageData)
        {
            using var content = new MultipartFormDataContent();
          var fileContent = new ByteArrayContent(imageData);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            content.Add(fileContent, "File", "profile.jpg");
            content.Add(new StringContent(userId.ToString()), "UserId");

            var response = await http.PostAsync("api/Customer/updateProfileImage", content);

            if (!response.IsSuccessStatusCode)
            {
              var error = await response.Content.ReadAsStringAsync();
              throw new Exception($"Image upload failed: {error}");
            }
        }
    }
}
