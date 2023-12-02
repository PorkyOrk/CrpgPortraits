using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using Serilog;

namespace CrpgP.Application;

public class SizeService
{
    private readonly ISizeRepository _repository;
    private readonly ILogger _logger;
    
    public SizeService(ISizeRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public Size FindSizeById(int id)
    {
        return _repository.GetById(id);
    }

    public IEnumerable<Size> FindSizeByDimensions(int width, int height)
    {
        return _repository.GetByDimensions(width, height);
    }

    public void InsertSize(Size size)
    {
        _repository.Insert(size);
    }

    public void UpdateSize(Size size)
    {
        _repository.Update(size);
    }
    
    public void DeleteSize(int id)
    {
        _repository.Delete(id);
    }

}