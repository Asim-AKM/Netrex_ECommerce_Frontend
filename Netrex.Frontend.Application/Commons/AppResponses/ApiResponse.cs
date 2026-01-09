using System.Net;

namespace Netrex.Frontend.Application.Commons.AppResponses
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Status { get; set; }
        public T Data { get; set; } = default!;


    }
}
