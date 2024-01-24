using System.Text.Json.Serialization;

namespace CrpgP.WebApplication.Models;

public class GameModel
{
    [JsonConstructor]
    public GameModel() { }

    public int Id { get; set; }
    public string Name { get; set; }
    public SizeModel PortraitSize { get; set; }
    public IEnumerable<TagModel>? Tags { get; set; }
}