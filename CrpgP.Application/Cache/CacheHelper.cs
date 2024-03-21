using Microsoft.Extensions.Caching.Memory;

namespace CrpgP.Application.Cache;

[Obsolete("Use CacheService instead.")]
public class CacheHelper<T>
{
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;
    
    public CacheHelper(IMemoryCache cache, int entryExpireSeconds)
    {
        _cache = cache;
        _cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(entryExpireSeconds));
    }
    
    public async Task<T?> GetEntryFromCacheOrRepository(object cacheKey, Func<Task<T?>> repositoryMethod)
    {
        if (_cache.TryGetValue(cacheKey, out T? result))
        {
            return result;
        }
        
        result = await repositoryMethod.Invoke();
        if (result != null)
        {
            _cache.Set(cacheKey, result, _cacheEntryOptions);
        }
        return result;
    }
    
    public T? GetEntryFromCache(object cacheKey)
    {
        return _cache.TryGetValue(cacheKey, out T? result) ? result : default;
    }
    
    
    public void AddEntryToCache(object cacheKey, T entry)
    {
        if (_cache.Get<T>(cacheKey) == null)
        {
            return;
        }
        
        // Remove existing then add new entry, to refresh the expiry time
        _cache.Remove(cacheKey);
        _cache.Set(cacheKey, entry);
    }
    
}