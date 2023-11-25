﻿using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Repositories;

public class GameRepository : Repository, IGameRepository
{
    public GameRepository(IConfiguration configuration) : base(configuration)
    {
    }
    
    public async Task<Game> GetByIdAsync(int gameId)
    {
        using (var conn = DbHelper.CreateConnection(ConnectionString))
        {
            const string sql = "SELECT * FROM games WHERE id = @GameId;";
            var output = conn.QueryFirstAsync<Game>(sql, new
            {
                GameId = gameId
            });

            return await output;
        }
    }
    
    public async Task<Game> GetByNameAsync(string gameName)
    {
        using (var conn = DbHelper.CreateConnection(ConnectionString))
        {
            const string sql = "SELECT * FROM games WHERE name = @GameName;";
            var output = conn.QueryFirstAsync<Game>(sql, new
            {
                GameName = gameName
            });

            return await output;
        }
    }
    
    public async Task InsertAsync(Game game)
    {
        using (var conn = DbHelper.CreateConnection(ConnectionString))
        {
            const string sql = "INSERT INTO games (name) VALUES (@GameName);";
            await conn.QueryAsync(sql, new
            {
                GameName = game.Name
            });
        }
    }
    
    public async Task UpdateAsync(Game game)
    {
        using (var conn = DbHelper.CreateConnection(ConnectionString))
        {
            const string updateGamesSql = "UPDATE games SET name = @GameName WHERE id = @GameId;";
            await conn.QueryAsync(updateGamesSql, new
            {
                GameName = game.Name,
                GameId = game.Id
            });
        }
    }

    public async Task DeleteAsync(int gameId)
    {
        using (var conn = DbHelper.CreateConnection(ConnectionString))
        {
            const string sql = "DELETE FROM games WHERE id = @GameId;";
            await conn.QueryFirstAsync<Game>(sql, new
            {
                GameId = gameId
            });
        }
    }
}