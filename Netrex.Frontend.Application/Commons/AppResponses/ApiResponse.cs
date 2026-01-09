using System.Net;

namespace Netrex.Frontend.Application.Commons.AppResponses
{
    public class ApiResponse<T>
    {
        public T Data { get; set; } = default!;
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public HttpStatusCode Status { get; set; }

        public static ApiResponse<T> Success(T data, string messege, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ApiResponse<T>
            {
                Data = data!,
                IsSuccess = true,
                Status = status,
                Message = messege
            };

        }

        public static ApiResponse<T> Fail(string error, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ApiResponse<T>
            {
                Data = default!,
                IsSuccess = false,
                Status = status,
                Message = error
            };
        }

        public static ApiResponse<T> Fail(T data, string error, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ApiResponse<T>
            {
                Data = data,
                IsSuccess = false,
                Status = status,
                Message = error
            };
        }
    }
}
