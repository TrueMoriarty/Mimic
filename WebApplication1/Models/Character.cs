namespace MimicWebApi.Models;

public class Character
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Money { get; set; }
    public int CreatorId { get; set; }
    public User Creator { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
}
