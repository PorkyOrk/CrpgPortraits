using CrpgP.Domain.Primitives;

namespace CrpgP.Domain.Entities;

public class Size: Entity
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Size()
    {
    }

    public Size(int id, int width, int height)
        : base(id)
    {
        Width = width;
        Height = height;
    }
}