using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using Serilog;

namespace CrpgP.Application;

public class PortraitService
{
    private readonly IPortraitRepository _repository;
    private readonly ILogger _logger;
    
    public PortraitService(IPortraitRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
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