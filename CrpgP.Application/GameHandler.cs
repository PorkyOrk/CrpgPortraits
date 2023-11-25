using System.Dynamic;
using System.Text.Json;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;

namespace CrpgP.Application;

public class GameHandler
{
    private readonly IGameRepository _repository;
    
    public GameHandler(IGameRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Game> FindGameByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
    
    public async Task<Game> FindGameByNameAsync(string name)
    {
        return await _repository.GetByNameAsync(name);
    }
    
    public async Task InsertGameAsync(string payload)
    {
        var game = MapJsonToGame(payload);
        
        if (game != null)
        {
            await _repository.InsertAsync(game);
        }
    }
    
    public async Task UpdateGameAsync(string? payload)
    {
        var game = MapJsonToGame(payload);

        if (game != null)
        {
            await _repository.UpdateAsync(game);
        }
    }

    public async Task DeleteGameAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }


    private static Game? MapJsonToGame(string? payload)
    {
        if (string.IsNullOrWhiteSpace(payload))
        {
            throw new ArgumentException("Empty payload.");
        }
        
        var game = JsonSerializer.Deserialize<Game>(payload, JsonSerializerOptions.Default);

        return game;
    }

}