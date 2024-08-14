using DAL.Dto.ItemDto;
using DAL.EfClasses;
using DAL.EfCode;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public interface IItemRepository : IGenericRepository<Item>
{
	public List<Item> GetPaged(PaginateDataItemDto paginateDataItemDto);
	public Item? TryDelete(int itemId, int? creatorId);
	public bool HasItemById(int itemId);
}

public class ItemRepository(MimicContext context) : GenericRepository<Item>(context), IItemRepository
{
	// TODO: Внести проверку CreatorId во все методы, чтобы минимизировать кол-во запросов к бд
	public List<Item> GetPaged(PaginateDataItemDto paginateDataItemDto)
	{
		var query = context.Items
			.Where(item => 
			paginateDataItemDto.SearchString == null
			|| item.Name == paginateDataItemDto.SearchString)
			.Skip((paginateDataItemDto.PageIndex - 1) 
			* paginateDataItemDto.PageSize)
			.Take(paginateDataItemDto.PageSize).AsNoTracking();

		List<Item> result = 
			paginateDataItemDto.OrderBy == nameof(Item.Name) 
			? query.OrderBy(item => item.Name).ToList()
			: query.ToList();

		return result;
	}

	public Item? TryDelete(int itemId, int? creatorId)
	{
		var query = context.Items.FirstOrDefault(item => item.ItemId == itemId 
			&& item.CreatorId == creatorId);
		if (query == null)
		{
			return null;
		}

		context.Items.Remove(query);
		context.SaveChanges();

		return query;
	}

	public bool HasItemById(int itemId) =>
		context.Items.Any(item => item.ItemId == itemId);

}