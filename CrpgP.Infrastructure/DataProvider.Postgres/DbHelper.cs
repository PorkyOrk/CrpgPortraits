using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CrpgP.Infrastructure.DataProvider.Postgres;

internal static class DbHelper
{
    internal static string GetConnectionString(IConfiguration configuration)
    {
        return configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentException("Missing PostgresDB connection string");
    }
    
    internal static NpgsqlConnection CreateConnection(string connectionString)
    {
        return new NpgsqlConnection(connectionString);
    }
}