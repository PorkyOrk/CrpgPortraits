namespace CrpgP.WebApplication.Models;

internal abstract class Game
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required Size PortraitSize { get; init; }
    public IEnumerable<Tag>? Tags { get; init; }
}