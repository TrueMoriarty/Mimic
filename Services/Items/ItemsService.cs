using DAL;
using DAL.EfClasses;
using Services.Items.Dto;
using Services.ItemProperties;
using DAL.Dto;
using DAL.Repositories;

namespace Services.Items;

public interface IItemsService
{
	Item CreateItem(ItemDto itemDto);
	List<Item> GetPagedItems(PaginateDataItemDto paginateDataItemDto);
	void DeleteItem(int itemId);
	bool HasItemById(int itemId);
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
	
	public List<Item> GetPagedItems(PaginateDataItemDto paginateDataItemDto)
	{
		List<Item> items = unitOfWork.ItemRepository.GetPaged(paginateDataItemDto);
		return items;
	}

	public void DeleteItem(int itemId)
	{
		unitOfWork.ItemRepository.Delete(itemId);
		unitOfWork.Save();
	}

	public bool HasItemById(int itemId) => 
		unitOfWork.ItemRepository.HasItemById(itemId);
}