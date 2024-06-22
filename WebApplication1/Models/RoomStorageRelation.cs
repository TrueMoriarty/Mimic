namespace MimicWebApi.Models;

public class RoomStorageRelation
{
    public int RoomId { get; set; }
    public Room Room { get; set; }

    public int StorageId { get; set; }
    public Storage Storage { get; set; }
}
