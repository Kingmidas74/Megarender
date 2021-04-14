using System;
using System.Threading;
using System.Threading.Tasks;

namespace Megarender.DataStorage
{
    public class DefaultCacheDataStorage: ICacheDataStorage
    {
        public async ValueTask CacheDataAsync<T>(string cacheKey, T data, TimeSpan cacheTime, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<T> RetriveDataAsync<T>(string cacheKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}