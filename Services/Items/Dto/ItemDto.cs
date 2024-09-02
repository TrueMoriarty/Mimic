using DAL.EfClasses;

namespace Services.Items.Dto;

public class ItemDto
{
    public int CreatorId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? StorageId { get; set; }
    public List<ItemProperty>? ItemProperties { get; set; }

    public Item MapToItem()
    {
        var item = new Item
        {
            CreatorId = CreatorId,
            Name = Name,
            Description = Description,
            StorageId = StorageId,
            Properties = ItemProperties
        };

        return item;
    }
}