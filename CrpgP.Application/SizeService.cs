using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;

namespace CrpgP.Application;

public class SizeService
{
    private readonly ISizeRepository _repository;
    
    public SizeService(ISizeRepository repository)
    {
        _repository = repository;
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