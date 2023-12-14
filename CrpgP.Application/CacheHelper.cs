using Microsoft.Extensions.Caching.Memory;

namespace CrpgP.Application;

public static class CacheHelper
{
    public static async Task<T?> GetEntryFromCacheOrRepository<T>(
        IMemoryCache cache, MemoryCacheEntryOptions options, object cacheKey, Func<Task<T?>> repositoryMethod)
    {
        if (!cache.TryGetValue(cacheKey, out T? result))
        {
            result = await repositoryMethod.Invoke();
            if (result != null)
            {
                cache.Set(cacheKey, result, options);
            }
        }
        return result;
    }
}