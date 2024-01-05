namespace CrpgP.Domain.Abstractions;

public interface IHealthCheckRepository
{
    public Task<bool> HaveConnectivity();
}