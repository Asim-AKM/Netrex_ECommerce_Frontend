using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.ProductManagement.Interfaces;
using Netrex.Frontend.Application.ViewModels.ProductManagement;
using static System.Net.Mime.MediaTypeNames;

public class CloudnaryManager : ICloudnaryManager
{
    private readonly HttpClient _httpClient;

    public CloudnaryManager(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<ApiResponse<T>> UploadToCloudinaryAsync<T>(
        List<byte[]> images,
        List<string> fileNames,
        string contentType)
    {
        try
        {
            using var content = new MultipartFormDataContent();

            for (int i = 0; i < images.Count; i++)
            {
                var ms = new MemoryStream(images[i]);
                var streamContent = new StreamContent(ms);
                streamContent.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

                var uniqueFileName = images.Count > 1
                    ? $"{Path.GetFileNameWithoutExtension(fileNames[i])}_{i}{Path.GetExtension(fileNames[i])}"
                    : fileNames[i];

                content.Add(streamContent, "files", uniqueFileName);
            }

            string url = images.Count > 1
                ? "api/Image/upload-batch"
                : "api/Image/upload";

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
               
                return ApiResponseDeserializer.FailResponse<T>(
                    $"Upload failed with status: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();
            return ApiResponseDeserializer.Deserialize<T>(json);
        }
        catch (Exception ex)
        {
           
            return ApiResponseDeserializer.FailResponse<T>($"Upload failed: {ex.Message}");
        }

    }
}


