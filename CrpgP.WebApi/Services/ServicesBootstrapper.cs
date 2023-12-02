using CrpgP.Application;
using CrpgP.Application.Health;
using CrpgP.Domain.Abstractions;
using CrpgP.Infrastructure.DataProvider.Postgres.Repositories;

namespace CrpgP.WebApi.Services;

public static class ServicesBootstrapper
{
    public static void RegisterServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // Database
        serviceCollection.AddNpgsqlDataSource(
            configuration.GetConnectionString("DefaultConnection") 
            ?? throw new InvalidOperationException("Missing connection string"));
        
        // Health Check
        serviceCollection.AddSingleton<IHealthCheckRepository, HealthCheckRepository>();
        serviceCollection.AddHealthChecks().AddCheck<HealthCheck>("HealthCheck");

        
        // Swagger
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
        
        // Infrastructure Repositories
        serviceCollection.AddSingleton<ITagRepository, TagRepository>();
        serviceCollection.AddSingleton<IPortraitRepository, PortraitRepository>();
        serviceCollection.AddSingleton<ISizeRepository, SizeRepository>();
        serviceCollection.AddSingleton<IGameRepository, GameRepository>();
        
        // Application Services
        serviceCollection.AddSingleton<GameService>();
        serviceCollection.AddSingleton<PortraitService>();
        serviceCollection.AddSingleton<SizeService>();
        serviceCollection.AddSingleton<TagService>();
    }
}