using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;
using Microsoft.Extensions.Configuration;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Repositories;

public class SizeRepository : Repository, ISizeRepository
{
    public SizeRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public Size GetById(int tagId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Size> GetByDimensions(int width, int height)
    {
        throw new NotImplementedException();
    }

    public void Insert(Size size)
    {
        throw new NotImplementedException();
    }

    public void Update(Size size)
    {
        throw new NotImplementedException();
    }

    public void Delete(int sizeId)
    {
        throw new NotImplementedException();
    }
}