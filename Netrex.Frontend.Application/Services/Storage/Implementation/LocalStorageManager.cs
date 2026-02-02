using Microsoft.JSInterop;
using Netrex.Frontend.Application.Services.Storage.Interface;
using System.Text.Json;
namespace Netrex.Frontend.Application.Services.Storage.Implementation
{
    public class LocalStorageManager(IJSRuntime _js) : ILocalStorageManager
    {

        public async Task SetAsync<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            await _js.InvokeVoidAsync("localStorage.setItem", key, json);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", key);
            return json == null ? default : JsonSerializer.Deserialize<T>(json);
        }

        public async Task RemoveAsync(string key)
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}
