namespace MimicWebApi.Models;

public class Room
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int MasterId { get; set; }
    public User Master { get; set; }
    
    public List<RoomStorageRelation> RoomStorageRelations { get; set; }
    public List<Character> Characters { get; set; }
}
