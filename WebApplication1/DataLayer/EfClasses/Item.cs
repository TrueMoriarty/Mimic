namespace MimicWebApi.DataLayer.Models;

public class Item
{
    public int ItemId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    // --------------------------------------
    // Relationships

    public int CreatorId { get; set; }
    public User Creator { get; set; }

    public int StorageId { get; set; }
    public Storage Storage { get; set; }

    public ICollection<Properties> Properties { get; set; }
}
