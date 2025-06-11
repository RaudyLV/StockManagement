

using System.Text.Json;
using Application.Interfaces;
using StackExchange.Redis;

namespace Infraestructure.Persistence.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<T?> GetAsync<T>(string cacheKey)
        {
            var value = await _database.StringGetAsync(cacheKey);
            return value.HasValue ? JsonSerializer.Deserialize<T>(value!) : default;
        }

        public async Task RemoveAsync(string cacheKey)
        {
            await _database.KeyDeleteAsync(cacheKey);
        }

        public async Task SetAsync<T>(string cacheKey, T value, TimeSpan? timeSpan = null)
        {
            var json = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(cacheKey, json, timeSpan);
        }
    }
}