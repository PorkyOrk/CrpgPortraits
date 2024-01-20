using System.Text.Json.Serialization;

namespace CrpgP.WebApplication.Models;

public class SizeModel
{
    [JsonConstructor]
    public SizeModel() { }
    
    public int Id { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
}