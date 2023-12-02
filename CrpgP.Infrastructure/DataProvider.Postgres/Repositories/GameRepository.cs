using CrpgP.Domain.Abstractions;
using CrpgP.Domain.Entities;
using CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;
using Dapper;
using Npgsql;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Repositories;

public class GameRepository : RepositoryBase, IGameRepository
{
    public GameRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
    }
    
    public async Task<Game?> FindByIdAsync(int gameId)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();

        const string sql =
            "SELECT * FROM games " +
            "LEFT JOIN sizes ON games.size_id=sizes.id " +
            "WHERE games.id = @GameId;";
        
        return cnn.QueryAsync<Game,Size,Game>(sql, (game, size) =>
            {
                game.PortraitSize = size; return game;
            }, new
            {
                GameId = gameId
            })
            .GetAwaiter()
            .GetResult()
            .FirstOrDefault();
    }
    
    public async Task<Game?> FindByNameAsync(string gameName)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql = 
            "SELECT * FROM games " +
            "LEFT JOIN sizes ON games.size_id=sizes.id " +
            "WHERE name = @GameName;";
        
        return cnn.QueryAsync<Game,Size,Game>(sql, (game, size) =>
            {
                game.PortraitSize = size; return game;
            }, new
            {
                GameName = gameName
            })
            .GetAwaiter()
            .GetResult()
            .FirstOrDefault();
    }
    
    public async Task<int> InsertAsync(Game game)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string sql =
            "INSERT INTO games (name, size_id) VALUES (@GameName, @SizeId);" +
            "SELECT currval('games_id_seq');";
        
        return conn.QueryAsync<int>(sql, new
            {
                GameName = game.Name,
                SizeId = game.PortraitSize.Id
            })
            .GetAwaiter()
            .GetResult()
            .FirstOrDefault();
    }
    
    public async Task UpdateAsync(Game game)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string sql = 
            "UPDATE games " +
            "SET name = @GameName, size_id = @SizeId " +
            "WHERE id = @GameId;";
        
        await conn.QueryAsync(sql, new
        {
            GameName = game.Name,
            SizeId = game.PortraitSize.Id,
            GameId = game.Id,
        });
    }

    public async Task DeleteAsync(int gameId)
    {
        await using var conn = await DataSource.OpenConnectionAsync();
        const string sql = "DELETE FROM games WHERE id = @GameId;";
        await conn.QueryAsync(sql, new
        {
            GameId = gameId
        });
    }
}