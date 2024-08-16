using DAL.Dto;
using DAL.EfClasses;

namespace DAL.Repositories.Interfaces;

public interface ICharacterRepository : IDisposable
{
    PaginatedContainerDto<List<Character>> GetPaginatedListByCreatorId(CharacterFilter filter);
    Character GetById(int id);
}