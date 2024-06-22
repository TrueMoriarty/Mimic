namespace MimicWebApi.Models;

public class Properties
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
