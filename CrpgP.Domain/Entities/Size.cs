namespace CrpgP.Domain.Entities;

public class Size
{
    public int Id { get; init; }
    public int Width { get; set; }
    public int Height { get; set; }
    
    public Size(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public Size(int id, int width, int height)
    {
        Id = id;
        Width = width;
        Height = height;
    }
}