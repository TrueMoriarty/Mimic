﻿using Services.Items.Dto;

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
            ItemId = ItemId,
            CreatorId = creatorId,
            StorageId = StorageId,
            Name = Name,
            Description = Description,
            ItemProperties = Properties?.ConvertAll(p => p.ToProperty())
        };

        return itemDto;
    }
}