using Netrex.Frontend.Application.Commons.AppResponses;
using System.Text.Json;

namespace Netrex.Frontend.Application.Commons
{
    public static class ApiResponseDeserializer
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public static ApiResponse<T> Deserialize<T>(string json)
        {
            try
            {
                var result = JsonSerializer.Deserialize<ApiResponse<T>>(json, _options);

                if (result == null)
                {
                    return FailResponse<T>("Unable to deserialize API response");
                }

                // Ensure Data is never null (CRITICAL)
                if (result.Data == null)
                {
                    result.Data = default!;
                }

                return result;
            }
            catch (JsonException ex)
            {
                return FailResponse<T>($"JSON Error: {ex.Message}");
            }
        }

        private static ApiResponse<T> FailResponse<T>(string message)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = message,
                Status = 500,
                Data = default!
            };
        }

    }
}
