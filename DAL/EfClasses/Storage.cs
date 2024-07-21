namespace DAL.EfClasses;

public class Storage
{
    public int StorageId { get; set; }
    public string? Description { get; set; }
    public string? Name { get; set; }

    // --------------------------------------
    // Relationships

    public ICollection<Item> Items { get; set; }
    public ICollection<RoomStorageRelation> RoomStorageRelations { get; set; }
}
