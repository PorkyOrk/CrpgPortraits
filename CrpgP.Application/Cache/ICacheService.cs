namespace CrpgP.Application.Cache;

public interface ICacheService
{
    public Task<T?> GetOrFetchEntryAsync<T>(string key, Func<Task<T?>> repositoryMethod);
    public void AddEntry<T>(string key, T entry);
    public void AddEntry<T>(string key, T entry, TimeSpan expiresIn);
    public void RemoveEntry(string key);
}