namespace MimicWebApi.DataLayer.Models;

public class Room
{
    public int RoomId { get; set; }
    public string? Name { get; set; }

    // --------------------------------------
    // Relationships

    public int MasterId { get; set; }
    public User Master { get; set; }

    public ICollection<RoomStorageRelation> RoomStorageRelations { get; set; }
    public ICollection<Character> Characters { get; set; }
}
