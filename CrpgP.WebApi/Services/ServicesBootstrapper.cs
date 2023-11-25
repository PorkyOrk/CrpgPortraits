using CrpgP.Application.Health;
using CrpgP.Domain.Abstractions;
using CrpgP.Infrastructure.DataProvider.Postgres.Repositories;

namespace CrpgP.WebApi.Services;

public static class ServicesBootstrapper
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        // Health Check
        // serviceCollection.AddHealthChecks().AddCheck<HealthCheck>("HealthCheck");
        serviceCollection.AddHealthChecks().AddCheck<DbHealthCheck>("DbHealthCheck");

        
        // Swagger
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
        
        // Repositories
        serviceCollection.AddSingleton<ITagRepository, TagRepository>();
        serviceCollection.AddSingleton<IPortraitRepository, PortraitRepository>();
        serviceCollection.AddSingleton<ISizeRepository, SizeRepository>();
        serviceCollection.AddSingleton<IGameRepository, GameRepository>();
    }
}