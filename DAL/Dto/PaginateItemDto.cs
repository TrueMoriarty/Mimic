namespace DAL.Dto;

public class PaginateDataItemDto
{
    public string? SearchString { get; set; }
 	public string? OrderBy { get; set; }
	public int PageIndex { get; set; }
	public int PageSize { get; set; }
}