using DAL.Dto;
using DAL.EfClasses;

namespace DAL.Repositories.Interfaces;

public interface ICharacterRepository : IGenericRepository<Character>
{
    PaginatedContainerDto<List<Character>> GetPaginatedListByCreatorId(CharacterFilter filter);
    Character GetById(int id, bool readOnly = true);

}