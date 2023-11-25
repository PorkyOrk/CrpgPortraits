using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;

namespace CrpgP.Application;

public class PortraitRepositoryHandler
{
    private readonly IPortraitRepository _repository;
    
    public PortraitRepositoryHandler(IPortraitRepository repository)
    {
        _repository = repository;
    }
    
    public Portrait FindPortraitById(int id)
    {
        return _repository.GetById(id);
    }

    public IEnumerable<Portrait> FindPortraitsByIds(int[] ids)
    {
        return _repository.GetByIds(ids);
    }

    public void InsertPortrait(Portrait portrait)
    {
        _repository.Insert(portrait);
    }

    public void UpdatePortrait(Portrait portrait)
    {
        _repository.Update(portrait);
    }
    
    public void DeletePortrait(int id)
    {
        _repository.Delete(id);
    }

}