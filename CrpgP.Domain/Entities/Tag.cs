namespace CrpgP.Domain.Entities;

public class Tag
{
    public int Id { get; init; }
    public required string Name { get; set;}

    public Tag()
    {
    }
    
    public Tag(string name)
    {
        Name = name;
    }
    
    public Tag(int id, string name)
    {
        Id = id;
        Name = name;
    }
}