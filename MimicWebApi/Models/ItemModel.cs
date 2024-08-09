using DAL.EfClasses;
using Services.Items.Dto;

namespace MimicWebApi.Models;

public class ItemModel : BaseModel
{
    public int? StorageId { get; set; }

    public List<ItemPropertyModel>? ItemProperties { get; set; }

    public ItemDto MapToItemDto(User creator)
    {
        var itemDto = new ItemDto
        {
            Creator = creator,
            StorageId = StorageId,
            Name = Name,
            Description = Description,
            ItemProperties = ItemProperties?.ConvertAll(p => p.ToProperty())
        };

        return itemDto;
    }
}