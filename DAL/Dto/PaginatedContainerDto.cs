using System.Collections;

namespace DAL.Dto;

public class PaginatedContainerDto<T>(T value, int totalCount, int totalPages) where T : ICollection
{
    public T Value { get; set; } = value;
    public int TotalCount { get; private set; } = totalCount;
    public int TotalPages { get; private set; } = totalPages;
}