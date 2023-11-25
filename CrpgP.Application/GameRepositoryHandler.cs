using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;

namespace CrpgP.Application;

public class GameRepositoryHandler
{
    private readonly IGameRepository _repository;
    
    public GameRepositoryHandler(IGameRepository repository)
    {
        _repository = repository;
    }
    
    public Game FindGameById(int id)
    {
        return _repository.GetById(id);
    }
    
    public Game FindGameByName(string name)
    {
        return _repository.GetByName(name);
    }
    
    public void InsertGame(Game game)
    {
        _repository.Insert(game);
    }
    
    public void UpdateGame(Game game)
    {
        _repository.Update(game);
    }

    public void DeleteGame(int id)
    {
        _repository.Delete(id);
    }
    


}