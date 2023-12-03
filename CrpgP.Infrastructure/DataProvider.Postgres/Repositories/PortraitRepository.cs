using System.Data;
using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;
using Dapper;
using Npgsql;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Repositories;

public class PortraitRepository : RepositoryBase, IPortraitRepository
{
    public PortraitRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
    }

    public async Task<Portrait?> FindByIdAsync(int portraitId)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql =
            "SELECT * FROM portraits " +
            "LEFT JOIN sizes ON portraits.size_id = sizes.id " +
            "WHERE portraits.id = @PortraitId;";
        
        var portrait = cnn.QueryAsync<Portrait,Size,Portrait>(sql, (portrait, size) => {
                portrait.Size = size;
                return portrait;
            }, new {
                PortraitId = portraitId
            })
            .GetAwaiter()
            .GetResult()
            .FirstOrDefault();

        if (portrait is not null)
        {
            portrait.Tags = await FindPortraitTags(cnn, portrait.Id);
        }

        return portrait;
    }

    public async Task<Dictionary<int,Portrait?>> FindByIdsAsync(IEnumerable<int> portraitIds)
    {
        var portraits = new Dictionary<int, Portrait?>();
        
        foreach (var id in portraitIds)
        {
            var p = await FindByIdAsync(id);
            portraits.Add(id, p);
        }

        return portraits;
    }

    public async Task<int> InsertAsync(Portrait portrait)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql =
            "INSERT INTO portraits (file_name, display_name, description, created, size_id) " +
            "VALUES (@FileName, @DisplayName, @Description, @Created, @SizeId);" +
            "SELECT currval('portraits_id_seq');";
        var id = cnn.QueryAsync<int>(sql, new {
            portrait.FileName,
            portrait.DisplayName,
            portrait.Description,
            portrait.Created,
            SizeId = portrait.Size.Id
        })
            .GetAwaiter()
            .GetResult()
            .FirstOrDefault();
        
        await InsertPortraitTags(cnn, id, portrait.Tags.Select(t => t.Id));
        return id;
    }

    public async Task UpdateAsync(Portrait portrait)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql =
            "UPDATE portraits " +
            "SET file_name = @FileName, display_name = @DisplayName, description = @Description, created = @Created, size_id = @SizeId " +
            "WHERE id = @PortraitId;";
        await cnn.QueryAsync(sql, new {
            portrait.FileName,
            portrait.DisplayName,
            portrait.Description,
            portrait.Created,
            SizeId = portrait.Size.Id,
            PortraitId = portrait.Id
        });
        
        await UpdatePortraitTags(cnn, portrait.Id, portrait.Tags.Select(t => t.Id));
    }

    public async Task DeleteAsync(int portraitId)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql = "DELETE FROM portraits WHERE id = @PortraitId;";
        await cnn.QueryAsync(sql, new {
            PortraitId = portraitId
        });
    }
    
    
    // Portrait Tags
    
    private static async Task<IEnumerable<Tag>> FindPortraitTags(IDbConnection cnn, int portraitId)
    {
        const string sql =
            "SELECT * FROM tags " +
            "WHERE id IN ( " +
            "SELECT tag_id FROM tag_portrait " +
            "WHERE tag_portrait.portrait_id = @PortraitId);";
        
        return await cnn.QueryAsync<Tag>(sql, new {
            PortraitId = portraitId
        });
    }
    
    private static async Task InsertPortraitTags(IDbConnection cnn, int portraitId, IEnumerable<int> tagIds)
    {
        const string sql = "INSERT INTO tag_portrait (tag_id, portrait_id) VALUES (@PortraitId, @TagId);";

        foreach (var tagId in tagIds)
        {
            await cnn.QueryAsync(sql, new {
                PortraitId = portraitId,
                TagId = tagId
            });
        }
    }
    
    private static async Task UpdatePortraitTags(IDbConnection cnn, int portraitId, IEnumerable<int> tagIds)
    {
        const string sqlDelete = "DELETE FROM tag_portrait WHERE portrait_id = @PortraitId;";
        const string sqlInsert = "INSERT INTO tag_portrait (tag_id, portrait_id) VALUES (@TagId, @PortraitId);";

        await cnn.QueryAsync(sqlDelete, new { PortraitId = portraitId });
        
        foreach (var tagId in tagIds)
        {
            await cnn.QueryAsync(sqlInsert, new { TagId = tagId, PortraitId = portraitId });
        }
    }
}