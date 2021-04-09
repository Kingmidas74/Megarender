using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Megarender.DataStorage
{
    public class RedisDataStorage:ICacheDataStorage
    {
        private readonly IDistributedCache _distributedCache;
        private readonly RedisSettings _redisSettings;

        public RedisDataStorage(IDistributedCache distributedCache, RedisSettings redisSettings)
        {
            _distributedCache = distributedCache;
            _redisSettings = redisSettings;
        }

        public async ValueTask CacheDataAsync<T>(string cacheKey, T data, TimeSpan cacheTime, CancellationToken cancellationToken = default)
        {
            if(data is null || !_redisSettings.Enabled) return;
            var serializedData = JsonSerializer.Serialize(data);
            await _distributedCache.SetStringAsync(cacheKey, serializedData,
                new DistributedCacheEntryOptions {AbsoluteExpirationRelativeToNow = cacheTime}, cancellationToken);
        }

        public async Task<T> RetriveDataAsync<T>(string cacheKey, CancellationToken cancellationToken = default)
        {
            var data = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
            return String.IsNullOrEmpty(data) ? default : JsonSerializer.Deserialize<T>(data);
        }
    }
}