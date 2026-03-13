using Microsoft.JSInterop;

namespace Netrex.Frontend.Blazor.Shared
{
    public class LogoutService
    {
        private readonly IJSRuntime _jsRuntime;

        public LogoutService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("submitLogoutForm");
        }
    }
}
