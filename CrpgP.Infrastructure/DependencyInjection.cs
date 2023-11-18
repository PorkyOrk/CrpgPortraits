using Microsoft.Extensions.DependencyInjection;

namespace CrpgP.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(c => c.RegisterServicesFromAssembly(assembly));

        return services;
    }
}