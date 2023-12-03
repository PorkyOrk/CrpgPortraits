using System.Data;
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
            "LEFT JOIN sizes ON games.size_id = sizes.id " +
            "WHERE games.id = @GameId;";
        
        var game = cnn.QueryAsync<Game,Size,Game>(sql, (game, size) => {
                game.PortraitSize = size;
                return game;
            }, new {
                GameId = gameId
            })
            .GetAwaiter()
            .GetResult()
            .FirstOrDefault();

        if (game is not null)
        {
            game.Tags = await FindGameTags(cnn, game.Id);
        }

        return game;
    }
    
    public async Task<Game?> FindByNameAsync(string gameName)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql = 
            "SELECT * FROM games " +
            "LEFT JOIN sizes ON games.size_id=sizes.id " +
            "WHERE name = @GameName;";
        
        var game = cnn.QueryAsync<Game,Size,Game>(sql, (game, size) => {
                game.PortraitSize = size;
                return game;
            }, new {
                GameName = gameName
            })
            .GetAwaiter()
            .GetResult()
            .FirstOrDefault();
        
        if (game is not null)
        {
            game.Tags = await FindGameTags(cnn, game.Id);
        }

        return game;
    }
    
    public async Task<int> InsertAsync(Game game)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql =
            "INSERT INTO games (name, size_id) VALUES (@GameName, @SizeId);" +
            "SELECT currval('games_id_seq');";
        
        var id = cnn.QueryAsync<int>(sql, new {
                GameName = game.Name,
                SizeId = game.PortraitSize.Id
            })
            .GetAwaiter()
            .GetResult()
            .FirstOrDefault();

        await InsertGameTags(cnn, id, game.Tags.Select(t => t.Id));

        return id;
    }
    
    public async Task UpdateAsync(Game game)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql = 
            "UPDATE games " +
            "SET name = @GameName, size_id = @SizeId " +
            "WHERE id = @GameId;";
        
        await cnn.QueryAsync(sql, new {
            GameName = game.Name,
            SizeId = game.PortraitSize.Id,
            GameId = game.Id,
        });

        await UpdateGameTags(cnn, game.Id, game.Tags.Select(t => t.Id));
    }

    public async Task DeleteAsync(int gameId)
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        const string sql = "DELETE FROM games WHERE id = @GameId;";
        await cnn.QueryAsync(sql, new {
            GameId = gameId
        });
    }
    
    
    // Game Tags
    
    private static async Task<IEnumerable<Tag>> FindGameTags(IDbConnection cnn, int gameId)
    {
        const string sql =
            "SELECT * FROM tags " +
            "WHERE id IN ( " +
            "SELECT tag_id FROM tag_game " +
            "WHERE tag_game.game_id = @GameId);";
        
        return await cnn.QueryAsync<Tag>(sql, new {
            GameId = gameId
        });
    }
    
    private static async Task InsertGameTags(IDbConnection cnn, int gameId, IEnumerable<int> tagIds)
    {
        const string sql = "INSERT INTO tag_game (tag_id, game_id) VALUES (@GameId, @TagId);";

        foreach (var tagId in tagIds)
        {
            await cnn.QueryAsync(sql, new {
                GameId = gameId,
                TagId = tagId
            });
        }
    }
    
    private static async Task UpdateGameTags(IDbConnection cnn, int gameId, IEnumerable<int> tagIds)
    {
        const string sqlDelete = "DELETE FROM tag_game WHERE game_id = @GameId;";
        const string sqlInsert = "INSERT INTO tag_game (tag_id, game_id) VALUES (@TagId, @GameId);";

        await cnn.QueryAsync(sqlDelete, new { GameId = gameId });
        
        foreach (var tagId in tagIds)
        {
            await cnn.QueryAsync(sqlInsert, new { TagId = tagId, GameId = gameId });
        }
    }
}