using CrpgP.Domain.Entities;

namespace CrpgP.Application;

public interface IPortraitRepository
{
    public Portrait GetById(int portraitId);
    public IEnumerable<Portrait> GetByIds(int[] portraitIds);
    public void Insert(Portrait portrait);
    public void Update(Portrait portrait);
    public void Delete(int portraitId);
}