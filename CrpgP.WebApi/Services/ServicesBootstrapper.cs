using CrpgP.Application;
using CrpgP.Application.Health;
using CrpgP.Domain.Abstractions;
using CrpgP.Infrastructure.DataProvider.Postgres.Repositories;
using CrpgP.WebApi.Options;

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
        
        
        
        // TODO
        // Get the config section
        var options = configuration.GetSection(nameof(MemoryCacheOptions))
            .Get<MemoryCacheOptions>();

        
        
        serviceCollection.Configure<GameService>(s =>
        {
            s.CacheEnabled = options.Enabled;
            s.CacheEntryExpireSeconds = options.EntryExpiryInSeconds;
        });
        serviceCollection.Configure<PortraitService>(s =>
        {
            s.CacheEnabled = options.Enabled;
            s.CacheEntryExpireSeconds = options.EntryExpiryInSeconds;
        });
        serviceCollection.Configure<SizeService>(s =>
        {
            s.CacheEnabled = options.Enabled;
            s.CacheEntryExpireSeconds = options.EntryExpiryInSeconds;
        });
        serviceCollection.Configure<TagService>(s =>
        {
            s.CacheEnabled = options.Enabled;
            s.CacheEntryExpireSeconds = options.EntryExpiryInSeconds;
        });


        // Options
        // Consider making a new options service which implements IConfigureOptions<TOptions>
        // serviceCollection.AddOptions<MemoryCacheOptions>("MemoryCacheOptions")
        //     .Configure<GameService, PortraitService, SizeService, TagService>(
        //         (options, gs, ps, ss, ts) => 
        //         {
        //             gs.CacheEnabled = options.Enabled;
        //             gs.CacheEntryExpireSeconds = options.EntryExpiryInSeconds;
        //             ps.CacheEnabled = options.Enabled;
        //             ps.CacheEntryExpireSeconds = options.EntryExpiryInSeconds;
        //             ss.CacheEnabled = options.Enabled;
        //             ss.CacheEntryExpireSeconds = options.EntryExpiryInSeconds;
        //             ts.CacheEnabled = options.Enabled;
        //             ts.CacheEntryExpireSeconds = options.EntryExpiryInSeconds;
        //         });
    }
}