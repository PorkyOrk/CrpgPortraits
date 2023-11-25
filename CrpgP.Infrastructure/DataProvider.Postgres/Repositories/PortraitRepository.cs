using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;
using Microsoft.Extensions.Configuration;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Repositories;

public class PortraitRepository : Repository, IPortraitRepository
{
    public PortraitRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public Portrait GetById(int portraitId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Portrait> GetByIds(int[] portraitIds)
    {
        throw new NotImplementedException();
    }

    public void Insert(Portrait portrait)
    {
        throw new NotImplementedException();
    }

    public void Update(Portrait portrait)
    {
        throw new NotImplementedException();
    }

    public void Delete(int portraitId)
    {
        throw new NotImplementedException();
    }
}