namespace CrpgP.WebApi.Options;

public class MemoryCacheOptions
{
    public bool Enabled { get; set; }
    public int EntryExpiryInSeconds { get; set; }
}