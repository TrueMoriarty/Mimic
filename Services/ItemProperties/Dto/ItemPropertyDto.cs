
using DAL.EfClasses;

namespace Services.ItemProperties.Dto;

public class ItemPropertyDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    // --------------------------------------
    // Relationships

    public int ItemId { get; set; }

    public ItemProperty MapToPropety()
    {
        ItemProperty property = new ItemProperty()
        {
            Name = Name,
            Description = Description,
            ItemId = ItemId
        };
        return property;
    }
}
