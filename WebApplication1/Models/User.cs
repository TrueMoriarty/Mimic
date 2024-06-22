namespace MimicWebApi.Models;

public class User
{
    public int Id { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Name { get; set; }


    public List<Item> Items { get; set; }
    public List<Room> Rooms { get; set; }
    public List<Character> Characters { get; set; }
}
