namespace CrpgP.WebApplication.Contracts;

internal sealed class Game
{
    public int Id { get; init; }
    public string Name { get; init; }
    public Size PortraitSize { get; set; }
    public IEnumerable<Tag> Tags { get; set; }
}