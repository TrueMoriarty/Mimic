using DAL;
using DAL.EfClasses;
using Services.Items.Dto;
using Services.Properties;

namespace Services.Items;

public interface IItemsService
{
    Item CreateItem(ItemDto itemDto);
}

public class ItemsService(IUnitOfWork unitOfWork, IPropertiesService propertiesService) : IItemsService
{
    public Item CreateItem(ItemDto itemDto)
    {
        var item = itemDto.MapToItem();

        unitOfWork.ItemRepository.Insert(item);
        unitOfWork.Save();

        if (itemDto.Properties is null) return item;
        AddProperties(item, itemDto.Properties);

        return item;
    }

    private void AddProperties(Item item, List<Property> properties)
    {
        properties.ForEach(p => p.ItemId = item.ItemId);
        item.Properties = propertiesService.CreateBulkProperties(properties);
    }
}