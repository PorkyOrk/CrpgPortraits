using Npgsql;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;

public abstract class Repository
{
    protected readonly NpgsqlDataSource DataSource;
    
    protected Repository(NpgsqlDataSource dataSource)
    {
        DataSource = dataSource;
    }
}