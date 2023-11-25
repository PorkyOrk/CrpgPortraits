using CrpgP.Domain.Entities;

namespace CrpgP.Domain.Abstractions;

public interface IGameRepository
{
    public Task<Game> GetByIdAsync(int gameId);
    public Task<Game> GetByNameAsync(string gameName);
    public Task InsertAsync(Game game);
    public Task UpdateAsync(Game game);
    public Task DeleteAsync(int gameId);
}