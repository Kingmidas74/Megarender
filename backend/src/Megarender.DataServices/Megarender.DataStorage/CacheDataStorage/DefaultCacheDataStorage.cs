using System;
using System.Threading;
using System.Threading.Tasks;

namespace Megarender.DataStorage
{
    public class DefaultCacheDataStorage: ICacheDataStorage
    {
        public ValueTask CacheDataAsync<T>(string cacheKey, T data, TimeSpan cacheTime, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> RetriveDataAsync<T>(string cacheKey, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}