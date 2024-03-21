using System.Text.Json;
using CrpgP.Application.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CrpgP.Application.Cache;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly bool _enabled;
    private readonly int _defaultEntryExpiryInSeconds;

    public CacheService(IMemoryCache cache, IOptions<MemoryCacheOptions> options)
    {
        _cache = cache;
        _enabled = options.Value.Enabled;
        _defaultEntryExpiryInSeconds = options.Value.EntryExpiryInSeconds;
    }
    
    public async Task<T?> GetOrFetchEntryAsync<T>(string key, Func<Task<T?>> repositoryMethod)
    {
        if (!_enabled)
        {
            return await repositoryMethod.Invoke();
        }
        
        if (_cache.TryGetValue(key, out var entry) && entry != null)
        {
            return JsonSerializer.Deserialize<T>((string) entry);
        }
        
        var result = await repositoryMethod.Invoke();
        if (result != null)
        {
            AddEntry(key, result);
        }

        return result;
    }
    
    // public T? GetOrFetchEntry<T>(string key, Func<Task<T>> repositoryMethod)
    // {
    //     if (_enabled)
    //     {
    //         if (_cache.TryGetValue(key, out var entry) && entry != null)
    //         {
    //             var result = JsonSerializer.Deserialize<T>((JsonDocument)entry);
    //             return result;
    //         }
    //     }
    //     return repositoryMethod.Invoke().GetAwaiter().GetResult();
    // }
    
    public void AddEntry<T>(string key, T entry)
    {
        if (_enabled && entry != null)
        {
            SerializeAndAdd(key, entry, TimeSpan.FromSeconds(_defaultEntryExpiryInSeconds));
        }
    }
    
    public void AddEntry<T>(string key, T entry, TimeSpan expiresIn)
    {
        if (_enabled && entry != null)
        {
            SerializeAndAdd(key, entry, expiresIn);
        }
    }
    
    public void RemoveEntry(string key)
    {
        _cache.Remove(key);
    }

    private void SerializeAndAdd(string key, object? entry, TimeSpan expiresIn)
    {
        var serialized = JsonSerializer.Serialize(entry);
        _cache.Set(key, serialized, expiresIn);
    }
}