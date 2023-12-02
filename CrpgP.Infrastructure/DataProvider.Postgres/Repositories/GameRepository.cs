using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;
using Dapper;
using Npgsql;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Repositories;

public class GameRepository : Repository, IGameRepository
{
    public GameRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
    }
    
    public async Task<Game?> FindByIdAsync(int gameId)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql = "SELECT * FROM games WHERE id = @GameId;";
        return await cnn.QueryFirstOrDefaultAsync<Game>(sql, new
        {
            GameId = gameId
        });
    }
    
    public async Task<Game?> FindByNameAsync(string gameName)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql = "SELECT * FROM games WHERE name = @GameName;";
        return await cnn.QueryFirstOrDefaultAsync<Game>(sql, new
        {
            GameName = gameName
        });
    }
    
    public async Task<int> InsertAsync(Game game)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string sql = "INSERT INTO games (name) VALUES (@GameName);" +
                           "SELECT currval('games_id_seq');";
        return await conn.QuerySingleAsync<int>(sql, new
        {
            GameName = game.Name
        });
    }
    
    public async Task UpdateAsync(Game game)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string updateGamesSql = "UPDATE games SET name = @GameName WHERE id = @GameId;";
        await conn.QueryAsync(updateGamesSql, new
        {
            GameName = game.Name,
            GameId = game.Id
        });
        
    }

    public async Task DeleteAsync(int gameId)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string sql = "DELETE FROM games WHERE id = @GameId;";
        await conn.QueryFirstAsync<Game>(sql, new
        {
            GameId = gameId
        });
    }
}