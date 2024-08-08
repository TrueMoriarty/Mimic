using DAL.EfClasses;
using Services.Properties.Dto;

namespace MimicWebApi.Models;

public class PropertyModel : BaseModel
{
    public int ItemId { get; set; }

    public PropertyDto MapToPropertyDto()
    {
        PropertyDto propertyDto = new PropertyDto()
        {
            Name = Name,
            Description = Description,
            ItemId = ItemId,
        };

        return propertyDto;
    }

    public Property ToProperty() => new()
    {
        Name = Name,
        Description = Description,
    };
}