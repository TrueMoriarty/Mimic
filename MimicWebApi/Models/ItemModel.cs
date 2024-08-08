using DAL.EfClasses;
using Services.Items.Dto;

namespace MimicWebApi.Models;

public class ItemModel : BaseModel
{
    public int? StorageId { get; set; }

    public List<PropertyModel>? Properties { get; set; }

    public ItemDto MapToItemDto(User creator)
    {
        var itemDto = new ItemDto
        {
            Creator = creator,
            StorageId = StorageId,
            Name = Name,
            Description = Description,
            Properties = Properties?.ConvertAll(p => p.ToProperty())
        };

        return itemDto;
    }
}