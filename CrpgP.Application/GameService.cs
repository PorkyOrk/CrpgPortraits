﻿using CrpgP.Application.Result;
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
    private const int CacheExpireSeconds = 120;
    
    
    public GameService(IGameRepository repository, IMemoryCache cache, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _cache = cache;
        _cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(CacheExpireSeconds));
    }
    
    public async Task<Result<Game>> GetGameByIdAsync(int id)
    {
        var game = await CacheHelper.GetEntryFromCacheOrRepository(
            _cache, _cacheEntryOptions, id, () => _repository.FindByIdAsync(id));
        
        return game is null 
            ? Result<Game>.Failure($"Game with id: {id} was not found.")
            : Result<Game>.Success(game);
    }
    
    public async Task<Result<Game>> GetGameByNameAsync(string name)
    {
        var game = await CacheHelper.GetEntryFromCacheOrRepository(
            _cache, _cacheEntryOptions, name, () => _repository.FindByNameAsync(name));
        
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