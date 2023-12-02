﻿using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;
using Dapper;
using Npgsql;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Repositories;

public class SizeRepository : RepositoryBase, ISizeRepository
{
    public SizeRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
    }

    public async Task<Size?> FindByIdAsync(int sizeId)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql = "SELECT * FROM sizes WHERE id = @SizeId;";
        return await cnn.QueryFirstOrDefaultAsync<Size>(sql, new
        {
            SizeId = sizeId
        });
    }

    public async Task<IEnumerable<Size>?> FindByDimensionsAsync(int width, int height)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql = "SELECT * FROM sizes WHERE width = @Width AND height = @Height ORDER BY width;";
        return await cnn.QueryAsync<Size>(sql, new
        {
            width,
            height
        });
    }

    public async Task<int> InsertAsync(Size size)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string sql =
            "INSERT INTO sizes (width, height) VALUES (@Width, @Height);" +
            "SELECT currval('sizes_id_seq');";
        return await conn.QuerySingleOrDefaultAsync<int>(sql, new
        {
            size.Width,
            size.Height
        });
    }

    public async Task UpdateAsync(Size size)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string sql = "UPDATE sizes SET width = @Width, height = @Height WHERE id = @SizeId;";
        await conn.QuerySingleAsync(sql, new
        {
            size.Width,
            size.Height,
            SizeId = size.Id
        });
    }

    public async Task DeleteAsync(int sizeId)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string sql = "DELETE FROM sizes WHERE id = @SizeId;";
        await conn.QuerySingleAsync(sql, new
        {
            SizeId = sizeId
        });
    }
}