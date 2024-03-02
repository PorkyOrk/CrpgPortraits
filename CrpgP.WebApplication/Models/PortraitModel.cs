using System.Text.Json.Serialization;

namespace CrpgP.WebApplication.Models;

public class PortraitModel
{
    [JsonConstructor]
    public PortraitModel() { }
    
    public int Id { get; init; }
    public required string FileName { get; init; }
    public SizeModel? SizeModel { get; init; }
    public string? DisplayName { get; init; }
    public string? Description { get; init; }
    public IEnumerable<TagModel> Tags { get; init; } = new List<TagModel>();
    public DateTime Created { get; init;  }
}