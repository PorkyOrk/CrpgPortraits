namespace CrpgP.WebApplication.Models;

internal abstract class Tag
{
    public int Id { get; init; }
    public required string Name { get; init;}
}