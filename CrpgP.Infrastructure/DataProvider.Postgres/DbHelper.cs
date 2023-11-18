using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CrpgP.Infrastructure.DataProvider.Postgres;

internal static class DbHelper
{
    internal static string GetConnectionString(IConfiguration config)
    {
        return config.GetConnectionString("PostgresDB") 
            ?? throw new ArgumentException("Missing PostgresDB connection string");
    }
    
    internal static NpgsqlConnection CreateConnection(string connectionString)
    {
        return new NpgsqlConnection(connectionString);
    }
}