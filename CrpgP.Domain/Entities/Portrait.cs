namespace CrpgP.Domain.Entities;

public class Portrait
{
    public int Id { get; init; }
    public required string FileName { get; set; }
    public required Size Size { get; set; }
    public string? DisplayName { get; set; }
    public string? Description { get; set; }
    
    public Tag[] Tags { get; set; } = Array.Empty<Tag>();
    public DateTime Created { get; }

    public Portrait()
    {
    }
    
    public Portrait(string fileName, string? displayName, string? description, Size size, DateTime created, Tag[] tags)
    {
        FileName = fileName;
        Size = size;
        DisplayName = displayName;
        Description = description;
        Tags = tags;
        Created = created;
    }

    public Portrait(int id, string fileName, string? displayName, string? description, Size size, DateTime created, Tag[] tags)
    {
        Id = id;
        FileName = fileName;
        Size = size;
        DisplayName = displayName;
        Description = description;
        Tags = tags;
        Created = created;
    }
}