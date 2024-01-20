using System.Text.Json.Serialization;

namespace CrpgP.WebApplication.Models;

public class TagModel
{
    [JsonConstructor]
    public TagModel() { }
    
    public int Id { get; init; }
    public required string Name { get; init;}
}