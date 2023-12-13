using CrpgP.Application.Result;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace CrpgP.Application;

public class GameService
{
    private readonly IGameRepository _repository;
    private readonly ILogger _logger;
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;
    
    public GameService(IGameRepository repository, IMemoryCache cache, ILogger logger)
    {
        _repository = repository;
        _cache = cache;
        _logger = logger;
        
        // Create options for cache entry
        const int expireCacheAfterSeconds = 120;
        _cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(expireCacheAfterSeconds));
    }

    // TODO: Move to its own file
    // Generic method to get object from cache, or database using provided delegate
    public async Task<T?> GetFromCacheOrRepository<T>(Func<Task<T?>> getFromRepositoryDelegate, object cacheKey)
    {
        if (!_cache.TryGetValue(cacheKey, out T? result))
        {
            result = await getFromRepositoryDelegate.Invoke();
            if (result != null)
            {
                _cache.Set(cacheKey, result, _cacheEntryOptions);
            }
        }
        return result;
    }
    
    public async Task<Result<Game>> GetGameByIdAsync(int id)
    {
        var game = await GetFromCacheOrRepository(() => _repository.FindByIdAsync(id), id);
        
        return game is null 
            ? Result<Game>.Failure($"Game with id: {id} was not found.")
            : Result<Game>.Success(game);
    }
    
    public async Task<Result<Game>> GetGameByNameAsync(string name)
    {
        var game = await _repository.FindByNameAsync(name);
        return game is null
            ? Result<Game>.Failure($"Game with name: {name} was not found.")
            : Result<Game>.Success(game);
    }
    
    public async Task<Result<int>> CreateGameAsync(string json)
    {
        try
        {
            var game = Validation.Mapper.MapToType<Game>(json);
            var result = await _repository.InsertAsync(game);
            return Result<int>.Success(result);
        }
        catch (Exception e)
        {
            _logger.Error("Game create failed. {0}", e.Message);
            return Result<int>.Failure(e.Message);
        }
    }
    
    public async Task<Result<object>> UpdateGameAsync(string json)
    {
        try
        {
            var game = Validation.Mapper.MapToType<Game>(json);
            await _repository.UpdateAsync(game);
            return Result<object>.Success();
        }
        catch (Exception e)
        {
            _logger.Error("Game update failed. {0}", e.Message);
            return Result<object>.Failure(e.Message);
        }
    }
    
    public async Task<Result<object>> DeleteGameAsync(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
            return Result<object>.Success();
        }
        catch (Exception e)
        {
            _logger.Error("Game delete failed. {0}", e.Message);
            return Result<object>.Failure(e.Message);
        }
    }
}