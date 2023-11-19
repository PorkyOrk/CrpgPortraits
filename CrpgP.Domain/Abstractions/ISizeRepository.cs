using CrpgP.Domain.Entities;

namespace CrpgP.Domain.Abstractions;

public interface ISizeRepository
{
    public Size GetById(int tagId);
    public IEnumerable<Size> GetByDimensions(int width, int height);
    public void Insert(Size tag);
    public void Update(Size tag);
    public void Delete(int tagId);
}