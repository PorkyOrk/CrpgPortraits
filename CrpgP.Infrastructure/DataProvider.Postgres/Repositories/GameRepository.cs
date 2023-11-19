using CrpgP.Domain.Abstractions;
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
    
    public Game GetById(int gameId)
    {
        using (var conn = DbHelper.CreateConnection(ConnectionString))
        {
            var sql =  "SELECT * FROM games WHERE id = @GameId;";
            var output = conn.QueryFirst<Game>(sql, new { GameId = gameId });

            return output;
        }
    }
    public Game GetByName(string gameName)
    {
        using (var conn = DbHelper.CreateConnection(ConnectionString))
        {
            var sql =  "SELECT * FROM games WHERE name = @GameName;";
            var output = conn.QueryFirst<Game>(sql, new { GameName = gameName });

            return output;
        }
    }

    
    
    
    public void Insert(Game game)
    {
        throw new NotImplementedException();
    }
    public void Update(Game game)
    {
        throw new NotImplementedException();
    }

    public void Delete(int gameId)
    {
        using (var conn = DbHelper.CreateConnection(ConnectionString))
        {
            var sql =  "DELETE * FROM games WHERE id = @GameId;";
            conn.QueryFirst<Game>(sql, new { GameId = gameId });
        }
    }
}