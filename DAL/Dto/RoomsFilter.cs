namespace DAL.Dto;

public class RoomsFilter : PaginatedFilter
{
    public int UserId { get; set; }
    public RoomType RoomType { get; set; }
}

public enum RoomType
{
    Created,
    Joined
}