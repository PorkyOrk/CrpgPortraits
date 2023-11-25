namespace CrpgP.Domain.Entities;

public class Game
{
    public int Id { get; init; }
    public string Name { get; init; }
    public Size PortraitSize { get; set; }

    public Game()
    {
    }
    
    public Game(string name)
    {
        Name = name;
    }
    
    public Game(string name, Size portraitSize)
    {
        Name = name;
        PortraitSize = portraitSize;
    }

    public Game(int id, string name, Size portraitSize)
    {
        Id = id;
        Name = name;
        PortraitSize = portraitSize;
    }
}