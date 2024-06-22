namespace MimicWebApi.Models;

public class Item
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public int CreatorId { get; set; }
    public User Creator { get; set; }
    
    public int StorageId { get; set; }
    public Storage Storage { get; set; }

    public List<Properties> Properties { get; set; }
}
