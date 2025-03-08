using DAL.Dto;
using DAL.EfClasses;

namespace DAL.Repositories.Interfaces;

public interface IRoomRepository : IGenericRepository<Room>
{
    PaginatedContainerDto<List<Room>> GetPaginatedRooms(RoomsFilter filter);
}