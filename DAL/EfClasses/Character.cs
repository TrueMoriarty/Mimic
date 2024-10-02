namespace DAL.EfClasses;

public class Character
{
    public int CharacterId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Money { get; set; }

    public DateTime CreateDate { get; set; }

    // --------------------------------------
    // Relationships

    public int CreatorId { get; set; }
    public User Creator { get; set; }
    public int? RoomId { get; set; }
    public Room? Room { get; set; }

    public int? StorageId { get; set; }
    public Storage? Storage { get; set; }
}