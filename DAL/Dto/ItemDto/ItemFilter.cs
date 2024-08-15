namespace DAL.Dto.ItemDto;

public class ItemFilter : PaginatedFilter
{
    public string? Name { get; set; }
 	public string? OrderBy { get; set; }
    public int? CreatorId { get; set; }
}