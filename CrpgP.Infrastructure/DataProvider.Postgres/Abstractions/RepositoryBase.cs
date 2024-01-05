using Npgsql;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;

public abstract class RepositoryBase
{
    protected readonly NpgsqlDataSource DataSource;

    private static bool _isConfigured;
    
    protected RepositoryBase(NpgsqlDataSource dataSource)
    {
        DataSource = dataSource;

        if (!_isConfigured)
        {
            // Dapper mapping config
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            _isConfigured = true;
        }
    }
}