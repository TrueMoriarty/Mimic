using DAL;
using DAL.EfClasses;
using Services.Items.Dto;
using Services.ItemProperties;
using DAL.Dto.ItemDto;
using DAL.Dto;
using System.ComponentModel.Design;

namespace Services.Items;

public interface IItemsService
{
	Item CreateItem(ItemDto itemDto);
	PaginatedContainerDto<List<Item>> GetPaginatedItems(ItemFilter paginateDataItemDto);
	Item? GetItemById(int itemId);
	void EditItem(int itemId, ItemDto itemDto);
	void DeleteItem(Item item);
	public Item? GetLightItemById(int itemId, int creatorId);
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

	public PaginatedContainerDto<List<Item>> GetPaginatedItems(ItemFilter paginateDataItemDto) =>
		unitOfWork.ItemRepository.GetPaginatedItems(paginateDataItemDto);

	public Item? GetItemById(int itemId) =>
		unitOfWork.ItemRepository.GetItemById(itemId);

	public void EditItem(int itemId, ItemDto itemDto)
	{
		Item item = itemDto.MapToItem();
		item.ItemId = itemId;
		unitOfWork.ItemRepository.Update(item);
		unitOfWork.Save();
	}

	public void DeleteItem(Item item)
	{
		unitOfWork.ItemRepository.DeleteItem(item);
		unitOfWork.Save();
	}

	public Item? GetLightItemById(int itemId, int creatorId)
	{
		Item? item = unitOfWork.ItemRepository.GetLightItemById(itemId, creatorId);
		return item;
	}
}