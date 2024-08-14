using DAL.Dto;
using DAL.Dto.ItemDto;
using DAL.EfClasses;
using DAL.EfCode;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public interface IItemRepository : IGenericRepository<Item>
{
	public PaginatedContainerDto<List<Item>> GetPaginatedItems(PaginateDataItemDto paginateDataItemDto);
	public Item? TryDelete(int itemId, int? creatorId);
}

internal class ItemRepository(MimicContext context) : GenericRepository<Item>(context), IItemRepository
{
	// TODO: Внести проверку CreatorId во все методы, чтобы минимизировать кол-во запросов к бд
	public PaginatedContainerDto<List<Item>> GetPaginatedItems(PaginateDataItemDto paginateDataItemDto)
	{
		var query = context.Items
			.Where(item => paginateDataItemDto.SearchString == null
				|| item.Name == paginateDataItemDto.SearchString)
			.Include(item => item.Properties).AsNoTracking();

		int totalCount = query.Count();

		var paginatedQuery = query
			.Skip((paginateDataItemDto.PageIndex - 1) 
				* paginateDataItemDto.PageSize)
			.Take(paginateDataItemDto.PageSize);

		var paginatedQueryOrdered = 
			paginateDataItemDto.OrderBy == nameof(Item.Name) 
			? paginatedQuery.OrderBy(item => item.Name)
			: paginatedQuery;

		var result = new PaginatedContainerDto<List<Item>>
		(
			paginatedQueryOrdered.ToList(),
			totalCount,
			(int) Math.Ceiling(totalCount / (double) paginateDataItemDto.PageSize)
		);

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

}