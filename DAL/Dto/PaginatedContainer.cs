namespace DAL.Dto;

public class PaginatedContainer<T>(T value, int totalCount, int totalPages)
{
    public T Value { get; set; } = value;
    public int TotalCount { get; private set; } = totalCount;
    public int TotalPages { get; private set; } = totalPages;
}