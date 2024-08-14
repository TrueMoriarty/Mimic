namespace DAL.Dto;

// TODO: Отнаследуйся от PaginateFilter
public class CharacterFilter
{
    public int? CreatorId { get; set; }
    public PaginatedFilter Pagination { get; set; }
}
