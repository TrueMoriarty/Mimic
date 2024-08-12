using DAL.Dto;
using DAL.EfClasses;
using DAL.EfCode;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public interface IItemRepository : IGenericRepository<Item>
{
	public List<Item> GetPaged(PaginateDataItemDto paginateDataItemDto);
}

public class ItemRepository(MimicContext context) : GenericRepository<Item>(context), IItemRepository
{
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
}