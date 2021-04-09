using System;
using System.Threading;
using System.Threading.Tasks;

namespace Megarender.DataStorage
{
    public interface ICacheDataStorage
    {
        ValueTask CacheDataAsync<T>(string cacheKey, T data, TimeSpan cacheTime, CancellationToken cancellationToken = default);
        Task<T> RetriveDataAsync<T>(string cacheKey, CancellationToken cancellationToken = default);

    }
}