using Microsoft.Extensions.Logging;
using Netrex.Frontend.Application.Commons;
using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.Services.ChatBot.Interfaces;
using Netrex.Frontend.Application.ViewModels.ChatBotVMS;
using Netrex.Frontend.Blazor.Services;
using System.Net.Http.Json;

namespace Netrex.Frontend.Application.Services.ChatBot.Implementations
{
    public class ChatBotManager : IChatBotManager
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ChatBotManager> _logger;

        // IHttpClientFactory inject karo
        public ChatBotManager(
            IHttpClientFactory httpClientFactory,
            ILogger<ChatBotManager> logger,
            LoaderService loaderService)
        {
            // "ApiClient" named client use karo - jo program.cs mein register hai
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _logger = logger;

            _logger.LogInformation("ChatBotManager initialized with ApiClient");
        }

        public async Task<ApiResponse<ChatResponseVM>> SendMessageAsync(string message)
        {
            try
            {
                _logger.LogInformation("Sending chat message: {Message}", message);

                var request = new ChatRequestVM { Message = message };

                // BaseAddress already set hai "ApiClient" mein
                // URL: BaseUrl + V1 = https://localhost:7239/api/v1/
                var response = await _httpClient.PostAsJsonAsync("chat/send", request);

                _logger.LogDebug("API Response Status: {StatusCode}", response.StatusCode);

                var json = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("API Response Content: {Json}", json);

                var result = ApiResponseDeserializer.Deserialize<ChatResponseVM>(json);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("API returned non-success status: {StatusCode}", response.StatusCode);
                    return ApiResponseDeserializer.FailResponse<ChatResponseVM>(
                        $"API returned {response.StatusCode}",
                        response.StatusCode);
                }

                return result;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Network error sending message to {Url}",
                    _httpClient.BaseAddress?.ToString() ?? "unknown");
                return ApiResponseDeserializer.FailResponse<ChatResponseVM>(
                    "Network error. Please check your connection and try again.",
                    System.Net.HttpStatusCode.ServiceUnavailable);
            }
            catch (TaskCanceledException timeoutEx)
            {
                _logger.LogError(timeoutEx, "Request timeout");
                return ApiResponseDeserializer.FailResponse<ChatResponseVM>(
                    "Request timed out. Please try again.",
                    System.Net.HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in SendMessageAsync");
                return ApiResponseDeserializer.FailResponse<ChatResponseVM>(
                    "An unexpected error occurred. Please try again.",
                    System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}