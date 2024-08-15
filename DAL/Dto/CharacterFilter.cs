namespace DAL.Dto;

public class CharacterFilter : PaginatedFilter
{
    public int? CreatorId { get; set; }
}