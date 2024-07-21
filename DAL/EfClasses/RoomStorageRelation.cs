namespace DAL.EfClasses;

public class RoomStorageRelation
{
    public int RoomStorageRelationId { get; set; }
    public int RoomId { get; set; }
    public int StorageId { get; set; }

    // --------------------------------------
    // Relationships

    public Room Room { get; set; }
    public Storage Storage { get; set; }
}
