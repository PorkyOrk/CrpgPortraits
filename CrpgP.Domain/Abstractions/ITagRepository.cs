using CrpgP.Domain.Entities;

namespace CrpgP.Domain.Abstractions;

public interface ITagRepository
{
    public Task<Tag?> FindByIdAsync(int tagId);
    public Task<Tag?> FindByNameAsync(string tagName);
    public Task<int> InsertAsync(Tag tag);
    public Task DeleteAsync(int tagId);
}