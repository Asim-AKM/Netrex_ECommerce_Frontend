using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.ProductManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.ProductManagement;

public class CloudnaryManager : ICloudnaryManager
{
    private readonly HttpClient _httpClient;

    public CloudnaryManager(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient"); // Named client use karo
    }

    public async Task<ApiResponse<CloudinaryUploadResult>> UploadImageToCloudinary(byte[] imageBytes, string fileName, string contentType)
    {
        using var content = new MultipartFormDataContent();
        using var ms = new MemoryStream(imageBytes);
        content.Add(new StreamContent(ms), "file", fileName);

        var response = await _httpClient.PostAsync("api/CloudinaryTest/upload-test", content);
        var json = await response.Content.ReadAsStringAsync();

        return ApiResponseDeserializer.Deserialize<CloudinaryUploadResult>(json);
    }
}
