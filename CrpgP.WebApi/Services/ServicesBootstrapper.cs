using CrpgP.Application;
using CrpgP.Application.Cache;
using CrpgP.Application.Health;
using CrpgP.Application.Options;
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

        // In-memory Cache
        serviceCollection.AddMemoryCache();
        serviceCollection.AddSingleton<ICacheService, CacheService>();
        
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
        
        
        // Register MemoryCacheOptions as options instance. Inject in a class with IOptions<MemoryCacheOptions>
        serviceCollection.Configure<MemoryCacheOptions>(configuration.GetSection(nameof(MemoryCacheOptions)));
    }
}