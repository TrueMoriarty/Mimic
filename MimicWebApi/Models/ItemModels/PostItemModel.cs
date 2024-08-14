using DAL.EfClasses;
using Services.Items.Dto;

namespace MimicWebApi.Models.ItemModels;

public class PostItemModel : BaseModel
{
    public int? StorageId { get; set; }

    public List<ItemPropertyModel>? ItemProperties { get; set; }

    public PostItemDto MapToPostItemDto(User creator)
    {
        var itemDto = new PostItemDto
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