using CrpgP.Domain.Entities;

namespace CrpgP.Domain.Abstractions;

public interface IGameRepository
{
    //public Task<Game> GetByIdAsync(int gameId);
    
    public Game GetById(int gameId);
    public Game GetByName(string gameName);
    public void Insert(Game game);
    public void Update(Game game);
    public void Delete(int gameId);
}