using CrpgP.Domain.Primitives;

namespace CrpgP.Domain.Entities;

public class Portrait : Entity
{
    public string FileName { get; set; }
    public string? DisplayName { get; set; }
    public string? Description { get; set; }
    
    public Size Size { get; set; }
    public Tag[] Tags { get; set; }
    public DateTime Created { get; }

    public Portrait()
    {
    }

    public Portrait(int id, string fileName, string? displayName, string? description, Size size, DateTime created, Tag[] tags)
        : base(id)
    {
        FileName = fileName;
        Size = size;
        DisplayName = displayName;
        Description = description;
        Tags = tags;
        Created = created;
    }
}