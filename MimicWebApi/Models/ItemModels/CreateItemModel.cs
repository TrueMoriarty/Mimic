using DAL.EfClasses;
using Services.Items.Dto;

namespace MimicWebApi.Models.ItemModels;

public class CreateItemModel : BaseModel
{
    public int? StorageId { get; set; }

    public List<ItemPropertyModel>? ItemProperties { get; set; }

    public CreateItemDto MapToItemDto(User creator)
    {
        var itemDto = new CreateItemDto
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