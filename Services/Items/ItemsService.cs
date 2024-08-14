using DAL;
using DAL.EfClasses;
using Services.Items.Dto;
using Services.ItemProperties;
using DAL.Dto.ItemDto;

namespace Services.Items;

public interface IItemsService
{
	Item CreateItem(PostItemDto itemDto);
	List<Item> GetPagedItems(PaginateDataItemDto paginateDataItemDto);
	Item? TryDeleteItem(int itemId, int? creatorId);
	bool HasItemById(int itemId);
}

public class ItemsService(IUnitOfWork unitOfWork, 
IItemPropertiesService propertiesService) : IItemsService
{
	public Item CreateItem(PostItemDto itemDto)
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
	
	public List<Item> GetPagedItems(PaginateDataItemDto paginateDataItemDto)
	{
		List<Item> items = unitOfWork.ItemRepository.GetPaged(paginateDataItemDto);
		return items;
	}

	public Item? TryDeleteItem(int itemId, int? creatorId)
	{
		var item = unitOfWork.ItemRepository.TryDelete(itemId, creatorId);
		unitOfWork.Save();
		return item;
	}

	public bool HasItemById(int itemId) => 
		unitOfWork.ItemRepository.HasItemById(itemId);
}