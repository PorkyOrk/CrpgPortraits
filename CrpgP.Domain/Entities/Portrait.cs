namespace CrpgP.Domain.Entities;

public class Portrait
{
    public int Id { get; }
    public string DisplayName { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string Description { get; set; }
    
    public Size Size { get; set; }
    public Size OriginalSize { get; set; }
    
    public Tag[] Tags { get; set; }
    public DateTime Created { get; }
}