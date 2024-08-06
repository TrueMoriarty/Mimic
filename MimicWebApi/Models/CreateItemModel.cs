namespace MimicWebApi.Models;

public class CreateItemModel
{
    public string Name { get; set; }
    public string Description { get; set; }

    public int? StorageId { get; set; }
}
