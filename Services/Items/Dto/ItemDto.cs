using DAL.EfClasses;

namespace Services.Items.Dto;

public class ItemDto
{
    public User Creator { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? StorageId { get; set; }
    public List<Property>? Properties { get; set; }

    public Item MapToItem()
    {
        var item = new Item
        {
            CreatorId = Creator.UserId,
            Name = Name,
            Description = Description,
            StorageId = StorageId
        };

        return item;
    }
}