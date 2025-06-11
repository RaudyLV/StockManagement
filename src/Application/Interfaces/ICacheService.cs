

namespace Application.Interfaces
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string cacheKey);
        Task SetAsync<T>(string cacheKey, T value, TimeSpan? timeSpan = null);
        Task RemoveAsync (string cacheKey);
    }
}