using DAL.Dto;
using DAL.EfClasses;
using DAL.EfCode;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

internal class RoomRepository(MimicContext context) : GenericRepository<Room>(context), IRoomRepository
{
    public PaginatedContainerDto<List<Room>> GetPaginatedRooms(RoomsFilter filter)
    {
        var queryRooms = context.Rooms
            .AsNoTracking()
            .Include(r => r.Master)
            .Where(r => r.MasterId == filter.UserId);

        var queryRoomsByCharacters = context.Characters
            .AsNoTracking()
            .Include(c => c.Room)
            .Where(c => c.RoomId != null && c.CreatorId == filter.UserId)
            .Select(c => c.Room!);

        var query = filter.RoomType switch { 
            RoomType.Created => queryRooms,
            RoomType.Joined => queryRoomsByCharacters,
            _ => throw new NotImplementedException() 
        };

        var paginatedList = query
            .OrderByDescending(e => e.RoomId)
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize);

        int totalCount = query.Count();

        return new PaginatedContainerDto<List<Room>>(paginatedList.ToList(),
            totalCount,
            (int) Math.Ceiling(totalCount / (double) filter.PageSize));
    }
}