namespace DAL.Dto;

public class CharacterFilter
{
    public int? CreatorId { get; set; }
    public PaginateFilter Pagination { get; set; }
}
