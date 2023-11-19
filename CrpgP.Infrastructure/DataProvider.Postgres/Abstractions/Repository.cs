using Microsoft.Extensions.Configuration;

namespace CrpgP.Infrastructure.DataProvider.Postgres.Abstractions;

public abstract class Repository
{
    protected readonly string ConnectionString;

    protected Repository(IConfiguration configuration)
    {
        ConnectionString = DbHelper.GetConnectionString(configuration);
    }
}