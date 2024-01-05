namespace CrpgP.Domain.Entities;

public class Game
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required Size PortraitSize { get; set; }
    public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();

    public Game()
    {
    }
    
    public Game(string name, IEnumerable<Tag> tags)
    {
        Name = name;
        Tags = tags;
    }
    
    public Game(string name, Size portraitSize, IEnumerable<Tag> tags)
    {
        Name = name;
        PortraitSize = portraitSize;
        Tags = tags;
    }

    public Game(int id, string name, Size portraitSize, IEnumerable<Tag> tags)
    {
        Id = id;
        Name = name;
        PortraitSize = portraitSize;
        Tags = tags;
    }
}