namespace CrpgP.Domain.Entities;

public class Game
{
    public int Id { get; }
    public string Name { get; set; }
    public Size PortraitSize { get; set; }
}