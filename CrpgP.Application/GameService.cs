using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using CrpgP.Application.Result;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;

namespace CrpgP.Application;

public class GameService
{
    private readonly IGameRepository _repository;
    
    public GameService(IGameRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<Game>> GetGameByIdAsync(int id)
    {
        var game = await _repository.FindByIdAsync(id);
        
        return game == null ?
            Result<Game>.Failure($"Game with the id: {id} was not found") :
            Result<Game>.Success(game);
    }
    
    public async Task<Result<Game>> GetGameByNameAsync(string name)
    {
        var game = await _repository.FindByNameAsync(name);
        
        return game == null ?
            Result<Game>.Failure($"Game with the name: {name} was not found") :
            Result<Game>.Success(game);
    }
    
    public async Task<Result<int>> CreateGameAsync(string payload)
    {
        var game = MapJsonToGame(payload);

        try
        {
            var result = await _repository.InsertAsync(game);
            return Result<int>.Success(result);
        }
        catch (Exception e)
        {
            return Result<int>.Failure(e.Message);
        }
    }

    public async Task<Result<object>> UpdateGameAsync(string payload)
    {
        var game = MapJsonToGame(payload);

        try
        {
            await _repository.UpdateAsync(game);
            return Result<object>.Success();
        }
        catch (Exception e)
        {
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
            return Result<object>.Failure(e.Message);
        }
    }

    private static Game MapJsonToGame(string? payload)
    {
        if (string.IsNullOrWhiteSpace(payload))
        {
            throw new ValidationException("Empty payload.");
        }
        
        var game = JsonSerializer.Deserialize<Game>(payload, JsonSerializerOptions.Default);
        
        if (game == null)
        {
            throw new ValidationException("Unable to map payload to Game object.");
        }

        return game;
    }

}