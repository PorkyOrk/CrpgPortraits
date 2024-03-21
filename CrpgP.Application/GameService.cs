using CrpgP.Application.Cache;
using CrpgP.Domain;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Domain.Errors;
using Serilog;

namespace CrpgP.Application;

public class GameService
{
    private readonly IGameRepository _repository;
    private readonly ILogger _logger;
    private readonly ICacheService _cacheService;

    public GameService(IGameRepository repository, ICacheService cacheService, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _cacheService = cacheService;
    }
    
    public async Task<Result> GetGameByIdAsync(int id)
    {
        var game = await _cacheService.GetOrFetchEntryAsync($"game-{id}", () => _repository.FindByIdAsync(id));
        return game is null 
            ? Result.Failure(GameErrors.NotFound(id))
            : Result.Success(game);
    }

    public async Task<Result> GetAllIds()
    {
        var gameIds = await _cacheService.GetOrFetchEntryAsync("game-ids", () => _repository.FindAllIdsAsync());
        return gameIds is null 
            ? Result.Failure(GameErrors.NoneFound())
            : Result.Success(gameIds);
    }
    
    public async Task<Result> GetGameByNameAsync(string name)
    {
        var game = await _cacheService.GetOrFetchEntryAsync($"game-{name}", () => _repository.FindByNameAsync(name));
        return game is null
            ? Result.Failure(GameErrors.NotFoundByName(name))
            : Result.Success(game);
    }
    
    public async Task<Result> CreateGameAsync(Game game)
    {
        try
        {
            var id = await _repository.InsertAsync(game);
            return Result.Success(id);
        }
        catch (Exception ex)
        {
            _logger.Error("Game create failed. {0}", ex.Message);
            return Result.Failure(GameErrors.CreateFailed());
        }
    }
    
    public async Task<Result> UpdateGameAsync(Game game)
    {
        try
        {
            _cacheService.RemoveEntry($"game-{game.Id}");
            await _repository.UpdateAsync(game);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.Error("Game update failed. {0}", ex.Message);
            return Result.Failure(GameErrors.UpdateFailed());
        }
    }
    
    public async Task<Result> DeleteGameAsync(int id)
    {
        try
        {
            _cacheService.RemoveEntry($"game-{id}");
            await _repository.DeleteAsync(id);
            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.Error("Game delete failed. {0}", e.Message);
            return Result.Failure(GameErrors.DeleteFailed());
        }
    }
}