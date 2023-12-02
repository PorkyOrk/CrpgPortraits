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
        const string sql = "SELECT * FROM portraits WHERE id = @PortraitId;";
        return await cnn.QueryFirstOrDefaultAsync<Portrait>(sql, new
        {
            PortraitId = portraitId
        });
    }

    public async Task<IEnumerable<Portrait>?> FindByIdsAsync(int[] portraitIds)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql = "SELECT * FROM portraits WHERE id IN @PortraitIds;";
        return await cnn.QueryAsync<Portrait>(sql, new
        {
            PortraitIds = portraitIds
        });
    }

    public async Task<int> InsertAsync(Portrait portrait)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string sql =
            "INSERT INTO portraits (file_name, display_name, description, date_created) " +
            "VALUES (@File_name, @Display_name, @Description, @Date_created);" +
            "SELECT currval('portraits_id_seq');";
        return await conn.QuerySingleOrDefaultAsync<int>(sql, new
        {
            File_name = portrait.FileName,
            Display_name = portrait.DisplayName,
            Description = portrait.Description,
            Date_created = portrait.Created
        });
    }

    public async Task UpdateAsync(Portrait portrait)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string sql =
            "UPDATE portraits " +
            "SET file_name = @File_name, display_name = @Display_name, description = @Description, date_created = @Date_created " +
            "WHERE id = @PortraitId;";
        await conn.QuerySingleAsync(sql, new
        {
            File_name = portrait.FileName,
            Display_name = portrait.DisplayName,
            Description = portrait.Description,
            Date_created = portrait.Created
        });
    }

    public async Task DeleteAsync(int portraitId)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string sql = "DELETE FROM portraits WHERE id = @PortraitId;";
        await conn.QuerySingleAsync(sql, new
        {
            PortraitId = portraitId
        });
    }
}