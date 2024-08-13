using DAL.Dto;
using DAL.EfClasses;

namespace DAL.Repositories.Interfaces;

public interface ICharacterRepository
{
    PaginatedContainer<List<Character>> GetPaginatedListByCreatorId(int creatorId, PaginateDataItemDto paginateDataItem);
    Character GetById(int id);
}