using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CrpgP.Application.Health;

public class DbHealthCheck : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var status = new DbStatus();
        var isHealthy = await status.IsHealthy();
        
        if (isHealthy)
        {
            return await Task.FromResult(HealthCheckResult.Healthy("A healthy result."));
        }

        return await Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "An unhealthy result."));
    }
}