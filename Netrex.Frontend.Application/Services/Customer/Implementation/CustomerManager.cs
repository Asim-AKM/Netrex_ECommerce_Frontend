using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.Customer.Interfaces;
using Netrex.Frontend.Application.Services.ProductManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.Customer;
using Netrex.Frontend.Application.ViewModels.ProductManagement;
using Netrex.Frontend.Blazor.Services;
using System.Net.Http.Json;

namespace Netrex.Frontend.Application.Services.Customer.Implementation
{
    public class CustomerManager : ICustomerManager
    {
        private readonly HttpClient _httpClient;
        private readonly LoaderService _loaderService;
        private readonly ICloudnaryManager _cloudinaryManager;

        public CustomerManager(
            IHttpClientFactory httpClientFactory,
            LoaderService loaderService,
            ICloudnaryManager cloudinaryManager)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _loaderService = loaderService;
            _cloudinaryManager = cloudinaryManager;
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                ".bmp" => "image/bmp",
                ".tiff" or ".tif" => "image/tiff",
                _ => "image/jpeg"
            };
        }

        public async Task<ApiResponse<string>> DeleteCustomer(Guid customerId)
        {
            try
            {
                _loaderService.Show();
                var response = await _httpClient.DeleteAsync($"api/Customer/DeleteCustomer/{customerId}");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<string>(json);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<string>(ex.Message);
            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<ApiResponse<List<VMCustomer>>> GetAllCustomers()
        {
            try
            {
                _loaderService.Show();
                var response = await _httpClient.GetAsync("api/Customer/GetAllCustomers");
                var json = await response.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<List<VMCustomer>>(json);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<List<VMCustomer>>(ex.Message);
            }
            finally
            {
                _loaderService.Hide();
            }
        }

        public async Task<ApiResponse<string>> UpdateCustomer(
            VMCustomer customer,
            byte[]? newImageBytes = null,
            string? newImageName = null)
        {
            if (customer == null)
                return ApiResponseDeserializer.FailResponse<string>("Customer cannot be null");

            string? oldPublicId = customer.Images?.CloudPublicId;

            try
            {
                _loaderService.Show();

                if (newImageBytes != null && newImageBytes.Length > 0 && !string.IsNullOrEmpty(newImageName))
                {
                    string contentType = GetContentType(newImageName);

                    var uploadResponse = await _cloudinaryManager.UploadToCloudinaryAsync<CloudinaryUploadResult>(
                        new List<byte[]> { newImageBytes },
                        new List<string> { newImageName },
                        contentType);

                    if (!uploadResponse.IsSuccess || uploadResponse.Data == null)
                        return ApiResponseDeserializer.FailResponse<string>($"Image upload failed: {uploadResponse.Message}");

                    if (customer.Images == null)
                        customer.Images = new ProfileImage();

                    customer.Images.ImageURL = uploadResponse.Data.Url ?? "";
                    customer.Images.CloudPublicId = uploadResponse.Data.CloudPublicId ?? "";

                    if (!string.IsNullOrEmpty(oldPublicId))
                    {
                        var deleteResponse = await _httpClient.DeleteAsync($"api/v1/Image/delete?publicId={oldPublicId}");
                        if (!deleteResponse.IsSuccessStatusCode)
                        {
                            var errorJson = await deleteResponse.Content.ReadAsStringAsync();
                            var errorResponse = ApiResponseDeserializer.Deserialize<bool>(errorJson);
                            return ApiResponseDeserializer.FailResponse<string>($"Failed to delete old image: {errorResponse?.Message ?? "Unknown error"}");
                        }
                    }
                }

                var apiResponse = await _httpClient.PutAsJsonAsync($"api/Customer/UpdateCustomer/{customer.UserId}", customer);
                var json = await apiResponse.Content.ReadAsStringAsync();
                return ApiResponseDeserializer.Deserialize<string>(json);
            }
            catch (Exception ex)
            {
                return ApiResponseDeserializer.FailResponse<string>(ex.Message);
            }
            finally
            {
                _loaderService.Hide();
            }
        }
    }
}