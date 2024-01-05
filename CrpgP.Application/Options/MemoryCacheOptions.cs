namespace CrpgP.Application.Options;

public class MemoryCacheOptions
{
    public bool Enabled { get; init; }
    public int EntryExpiryInSeconds { get; init; }
}