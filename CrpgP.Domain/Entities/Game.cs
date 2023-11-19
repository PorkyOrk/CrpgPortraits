using CrpgP.Domain.Primitives;

namespace CrpgP.Domain.Entities;

public class Game : Entity
{
    public string Name { get; set; }
    public Size PortraitSize { get; set; }

    public Game(int id, string name, Size portraitSize)
        :base(id)
    {
        Name = name;
        PortraitSize = portraitSize;
    }
}