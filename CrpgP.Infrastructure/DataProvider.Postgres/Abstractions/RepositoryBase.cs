using Npgsql;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;

public abstract class RepositoryBase
{
    protected readonly NpgsqlDataSource DataSource;
    
    protected RepositoryBase(NpgsqlDataSource dataSource)
    {
        DataSource = dataSource;
    }
}