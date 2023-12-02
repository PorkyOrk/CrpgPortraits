using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Repositories;

public class TagRepository : RepositoryBase, ITagRepository
{

    public TagRepository(NpgsqlDataSource dataSource) : base(dataSource) 
    {
    }
    
    
    public Tag GetById(int tagId)
    {
        throw new NotImplementedException();
    }

    public Tag GetByName(string tagName)
    {
        throw new NotImplementedException();
    }

    public void Insert(Tag tag)
    {
        throw new NotImplementedException();
    }

    public void Update(Tag tag)
    {
        throw new NotImplementedException();
    }

    public void Delete(int tagId)
    {
        throw new NotImplementedException();
    }
}