namespace DAL.EfClasses;

public class User
{
    public int UserId { get; set; }
    public string? ExternalUserId { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Name { get; set; }

    // --------------------------------------
    // Relationships

    public ICollection<Item> Items { get; set; }
    public ICollection<Room> Rooms { get; set; }
    public ICollection<Character> Characters { get; set; }
}
