using DAL.Dto;
using DAL.Dto.ItemDto;
using DAL.EfClasses;
using DAL.EfCode;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public interface IItemRepository : IGenericRepository<Item>
{
	public PaginatedContainerDto<List<Item>> GetPaginatedItems(ItemFilter paginateDataItemDto);
	public Item? TryDelete(int itemId, int? creatorId);
}

internal class ItemRepository(MimicContext context) : GenericRepository<Item>(context), IItemRepository
{
	public PaginatedContainerDto<List<Item>> GetPaginatedItems(ItemFilter paginateDataItemDto)
	{
		var query = context.Items
			.Where(item => (paginateDataItemDto.Name == null
				|| item.Name == paginateDataItemDto.Name) 
				&& (paginateDataItemDto.CreatorId == null 
				||item.CreatorId == paginateDataItemDto.CreatorId))
			.Include(item => item.Properties).AsNoTracking();

		int totalCount = query.Count();

		var queryOrdered = 
			paginateDataItemDto.OrderBy == nameof(Item.Name) 
			? query.OrderBy(item => item.Name)
			: query;

		var paginatedQueryOrdered = queryOrdered
			.Skip((paginateDataItemDto.PageIndex - 1) 
				* paginateDataItemDto.PageSize)
			.Take(paginateDataItemDto.PageSize);

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