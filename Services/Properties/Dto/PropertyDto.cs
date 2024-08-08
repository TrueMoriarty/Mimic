
using DAL.EfClasses;

namespace Services.Properties.Dto;

public class PropertyDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    // --------------------------------------
    // Relationships

    public int ItemId { get; set; }

    public Property MapToPropety()
    {
        Property property = new Property()
        {
            Name = Name,
            Description = Description,
            ItemId = ItemId
        };
        return property;
    }
}
