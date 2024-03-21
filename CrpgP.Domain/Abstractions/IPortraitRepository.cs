using CrpgP.Domain.Entities;

namespace CrpgP.Domain.Abstractions;

public interface IPortraitRepository
{
    public Task<Portrait?> FindByIdAsync(int portraitId);
    public Task<Dictionary<int,Portrait>?> FindByIdsAsync(IEnumerable<int> portraitIds);
    public Task<IEnumerable<Portrait>?> FindAllPage(int page, int count);
    public Task<int> CountAll();
    public Task<int> InsertAsync(Portrait portrait);
    public Task UpdateAsync(Portrait portrait);
    public Task DeleteAsync(int portraitId);
}