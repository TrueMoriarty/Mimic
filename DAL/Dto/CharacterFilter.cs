namespace DAL.Dto;

public class CharacterFilter
{
    public int? CreatorId { get; set; }
    public PaginatedFilter Pagination { get; set; }
}
