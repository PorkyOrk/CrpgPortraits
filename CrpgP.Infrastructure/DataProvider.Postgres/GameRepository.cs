using CrpgP.Application.Repositories;
using CrpgP.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace CrpgP.Infrastructure.DataProvider.Postgres;
using Dapper;

public class GameRepository : IGameRepository
{
    private readonly string _connectionString;
    
    protected GameRepository(IConfiguration config)
    {
        _connectionString = DbHelper.GetConnectionString(config);
    }

    public Task<Game> GetByIdAsync(int id)
    {
        using (var conn = DbHelper.CreateConnection(_connectionString))
        {
            var sql =  "SELECT * FROM OrderDetails WHERE OrderDetailID = @GameId;";
            var output = conn.QueryFirstAsync<Game>(sql, new {GameId = id});

            return output;
        }
    }
    

    
}