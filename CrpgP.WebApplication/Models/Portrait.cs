namespace CrpgP.WebApplication.Models;

internal abstract class Portrait
{
    public int Id { get; init; }
    public required string FileName { get; init; }
    public required Size Size { get; init; }
    public string? DisplayName { get; init; }
    public string? Description { get; init; }
    public IEnumerable<Tag> Tags { get; init; } = new List<Tag>();
    public DateTime Created { get; init;  }
}