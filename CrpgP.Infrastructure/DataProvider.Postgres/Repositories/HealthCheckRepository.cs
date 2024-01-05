using System.Data;
using CrpgP.Domain.Abstractions;
using CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;
using Npgsql;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Repositories;

public class HealthCheckRepository : RepositoryBase, IHealthCheckRepository
{
    public HealthCheckRepository(NpgsqlDataSource dataSource) : base(dataSource)
    {
    }

    public async Task<bool> HaveConnectivity()
    {
        await using var cnn = await DataSource.OpenConnectionAsync();
        return cnn.State == ConnectionState.Open;
    }
}