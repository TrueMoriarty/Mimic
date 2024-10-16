using DAL.EfClasses;
using Services.Items.Dto;

namespace MimicWebApi.Models.ItemModels;

public class ItemModel : BaseModel
{
    public int? ItemId { get; set; }
    public int CreatorId { get; set; }
    public int? StorageId { get; set; }

    public List<ItemPropertyModel>? Properties { get; set; }

    public ItemDto MapToItemDto(int creatorId)
    {
        var itemDto = new ItemDto
        {
            CreatorId = creatorId,
            StorageId = StorageId,
            Name = Name,
            Description = Description,
            ItemProperties = Properties?.ConvertAll(p => p.ToProperty())
        };

        return itemDto;
    }

    public Item MapToItem(int creatorId)
    {
        var item = new Item
        {
            ItemId = ItemId ?? 0,
            CreatorId = creatorId,
            StorageId = StorageId,
            Name = Name,
            Description = Description,
            Properties = Properties?.ConvertAll(p => p.ToProperty())
        };

        return item;
    }
}