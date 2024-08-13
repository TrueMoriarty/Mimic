using DAL;
using DAL.Dto;
using DAL.EfClasses;

namespace Services;

public interface ICharactersService
{
    PaginatedContainer<List<Character>> GetListByCreatorId(CharacterFilter filter);
    Character? GetById(int characterId);
}

public class CharactersService(IUnitOfWork unitOfWork) : ICharactersService
{
    public PaginatedContainer<List<Character>> GetListByCreatorId(CharacterFilter filter) =>
        unitOfWork.CharactersRepository.GetPaginatedListByCreatorId(filter);

    public Character? GetById(int characterId) =>
        unitOfWork.CharactersRepository.GetById(characterId);
}