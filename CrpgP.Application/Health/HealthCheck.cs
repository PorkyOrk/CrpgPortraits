using CrpgP.Domain.Abstractions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CrpgP.Application.Health;

public class HealthCheck : IHealthCheck
{
    private readonly IHealthCheckRepository _repository;
    
    public HealthCheck(IHealthCheckRepository repository)
    {
        _repository = repository;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = await _repository.HaveConnectivity();
        
        if (isHealthy)
        {
            return await Task.FromResult(HealthCheckResult.Healthy("A healthy result."));
        }

        return await Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "An unhealthy result."));
    }
}