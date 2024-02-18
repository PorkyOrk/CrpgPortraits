using CrpgP.Domain.Entities;

namespace CrpgP.Domain.Abstractions;

public interface IGameRepository
{
    public Task<IEnumerable<int>?> FindAllIdsAsync();
    public Task<Game?> FindByIdAsync(int gameId);
    public Task<Game?> FindByNameAsync(string gameName);
    public Task<int> InsertAsync(Game game);
    public Task UpdateAsync(Game game);
    public Task DeleteAsync(int gameId);
}