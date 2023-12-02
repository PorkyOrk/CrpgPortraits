using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;
using Dapper;
using Npgsql;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Repositories;

public class TagRepository : RepositoryBase, ITagRepository
{

    public TagRepository(NpgsqlDataSource dataSource) : base(dataSource) 
    {
    }
    
    public async Task<Tag?> FindByIdAsync(int tagId)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql = "SELECT * FROM tags WHERE id = @TagId;";
        return await cnn.QueryFirstOrDefaultAsync<Tag>(sql, new
        {
            TagId = tagId
        });
    }

    public async Task<Tag?> FindByNameAsync(string tagName)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql = "SELECT * FROM tags WHERE name = @TagName;";
        return await cnn.QueryFirstOrDefaultAsync<Tag>(sql, new
        {
            TagName = tagName
        });
    }

    public async Task<int> InsertAsync(Tag tag)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string sql =
            "INSERT INTO tags (name) VALUES (@TagName);" +
            "SELECT currval('tags_id_seq');";
        return await conn.QuerySingleOrDefaultAsync<int>(sql, new
        {
            TagName = tag.Name
        });
    }
    
    public async Task DeleteAsync(int tagId)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string sql = "DELETE FROM tags WHERE id = @TagId;";
        await conn.QuerySingleAsync(sql, new
        {
            TageId = tagId
        });
    }
}