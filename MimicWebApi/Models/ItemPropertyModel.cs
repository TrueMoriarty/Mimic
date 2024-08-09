using DAL.EfClasses;
using Services.ItemProperties.Dto;

namespace MimicWebApi.Models;

public class ItemPropertyModel : BaseModel
{
    public int ItemId { get; set; }

    public ItemPropertyDto MapToItemPropertyDto()
    {
        ItemPropertyDto propertyDto = new ItemPropertyDto()
        {
            Name = Name,
            Description = Description,
            ItemId = ItemId,
        };

        return propertyDto;
    }

    public ItemProperty ToProperty() => new()
    {
        Name = Name,
        Description = Description,
    };
}