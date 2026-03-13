using Netrex.Frontend.Application.Commons.AppResponses;
using System.Net;

namespace Netrex.Frontend.Application.Commons.SharedClasses
{
    public class BaseManager
    {
        protected bool TryGetErrorResponse<T>(
            HttpResponseMessage response,
            out ApiResponse<T> errorResponse)
        {
            errorResponse = response.StatusCode switch
            {
                HttpStatusCode.Unauthorized =>
                    ApiResponseDeserializer.FailResponse<T>(
                        "unauthorized", HttpStatusCode.Unauthorized),
                HttpStatusCode.Forbidden =>
                    ApiResponseDeserializer.FailResponse<T>(
                        "forbidden", HttpStatusCode.Forbidden),
                HttpStatusCode.ServiceUnavailable =>
                    ApiResponseDeserializer.FailResponse<T>(
                        "network_error", HttpStatusCode.ServiceUnavailable),
                _ => null!
            };

            // True = error hai, False = normal response hai
            return errorResponse != null;
        }
    }
}
