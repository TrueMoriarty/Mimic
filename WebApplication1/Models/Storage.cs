namespace MimicWebApi.Models;

public class Storage
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Name { get; set; }

    public List<Item> Items { get; set; }
    public List<RoomStorageRelation> RoomStorageRelations { get; set; }
}
