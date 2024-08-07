using DAL;
using DAL.EfClasses;
using Services.Items.Dto;

namespace Services.Items;

public interface IItemsService
{
    Item CreateItem(CreateItemDto createItemDto);
}

public class ItemsService(IUnitOfWork unitOfWork, IPropertiesService propertiesService) : IItemsService
{
    public Item CreateItem(CreateItemDto createItemDto)
    {
        var item = createItemDto.ToItem();

        unitOfWork.ItemRepository.Insert(item);
        unitOfWork.Save();

        if (createItemDto.Properties is null) return item;
        AddProperties(item, createItemDto.Properties);

        return item;
    }

    private void AddProperties(Item item, List<Property> properties)
    {
        properties.ForEach(p => p.ItemId = item.ItemId);
        item.Properties = propertiesService.CreateBulkProperties(properties);
    }
}