using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;

namespace CrpgP.Application.Games;

public class GameQueryHandler
{
    private IGameRepository _repository;

    public GameQueryHandler(IGameRepository repository)
    {
        _repository = repository;
    }


    public Game FindGameById(int id)
    {
        return _repository.GetById(id);
    }
    
}