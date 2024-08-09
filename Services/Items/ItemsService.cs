using DAL;
using DAL.EfClasses;
using Services.Items.Dto;
using Services.ItemProperties;

namespace Services.Items;

public interface IItemsService
{
    Item CreateItem(ItemDto itemDto);
}

public class ItemsService(IUnitOfWork unitOfWork, 
IItemPropertiesService propertiesService) : IItemsService
{
    public Item CreateItem(ItemDto itemDto)
    {
        var item = itemDto.MapToItem();

        unitOfWork.ItemRepository.Insert(item);
        unitOfWork.Save();

        if (itemDto.ItemProperties is null) return item;
        AddProperties(item, itemDto.ItemProperties);

        return item;
    }

    private void AddProperties(Item item, List<ItemProperty> properties)
    {
        properties.ForEach(p => p.ItemId = item.ItemId);
        item.Properties = propertiesService.CreateBulkItemProperties(properties);
    }
}