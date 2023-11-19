using CrpgP.Domain.Primitives;

namespace CrpgP.Domain.Entities;

public class Tag : Entity
{
    public string Name { get; set;}

    public Tag(int id, string name) : base(id)
    {
        Name = name;
    }
}