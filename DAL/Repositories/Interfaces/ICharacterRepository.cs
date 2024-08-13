using DAL.Dto;
using DAL.EfClasses;

namespace DAL.Repositories.Interfaces;

public interface ICharacterRepository
{
    PaginatedContainer<List<Character>> GetPaginatedListByCreatorId(CharacterFilter filter);
    Character GetById(int id);
}