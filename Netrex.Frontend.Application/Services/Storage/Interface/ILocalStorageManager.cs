namespace Netrex.Frontend.Application.Services.Storage.Interface
{
    public interface ILocalStorageManager
    {
        Task SetAsync<T>(string key, T value);
        Task<T?> GetAsync<T>(string key);
        Task RemoveAsync(string key);
    }
}
