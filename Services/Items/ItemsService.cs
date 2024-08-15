using DAL;
using DAL.EfClasses;
using Services.Items.Dto;
using Services.ItemProperties;
using DAL.Dto.ItemDto;
using DAL.Dto;

namespace Services.Items;

public interface IItemsService
{
	Item CreateItem(CreateItemDto itemDto);
	PaginatedContainerDto<List<Item>> GetPaginatedItems(ItemFilter paginateDataItemDto);
	Item? TryDeleteItem(int itemId, int? creatorId);
}

public class ItemsService(
    IUnitOfWork unitOfWork,
    IItemPropertiesService propertiesService) : IItemsService
{
	public Item CreateItem(CreateItemDto itemDto)
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
	
	public PaginatedContainerDto<List<Item>> GetPaginatedItems(ItemFilter paginateDataItemDto) => 
		unitOfWork.ItemRepository.GetPaginatedItems(paginateDataItemDto);

	public Item? TryDeleteItem(int itemId, int? creatorId)
	{
		var item = unitOfWork.ItemRepository.TryDelete(itemId, creatorId);
		unitOfWork.Save();
		return item;
	}
}