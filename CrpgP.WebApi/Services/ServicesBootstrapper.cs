using CrpgP.Domain.Abstractions;
using CrpgP.Infrastructure.DataProvider.Postgres.Repositories;

namespace CrpgP.WebApi.Services;

public static class ServicesBootstrapper
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        // Health Check
        serviceCollection.AddHealthChecks();
        
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