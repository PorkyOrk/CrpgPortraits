using CrpgP.Domain.Entities;

namespace CrpgP.Domain.Abstractions;

public interface ISizeRepository
{
    public Task<Size?> FindByIdAsync(int tagId);
    public Task<IEnumerable<Size>?> FindByDimensionsAsync(int width, int height);
    public Task<int> InsertAsync(Size tag);
    public Task UpdateAsync(Size tag);
    public Task DeleteAsync(int tagId);
}