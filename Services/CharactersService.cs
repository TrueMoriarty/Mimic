using DAL;
using DAL.Dto;
using DAL.EfClasses;

namespace Services;

public interface ICharactersService
{
    PaginatedContainer<List<Character>> GetListByCreatorId(int creatorId, PaginateDataItemDto paginateDataItemDto);
    Character? GetById(int characterId);
}

public class CharactersService(IUnitOfWork unitOfWork) : ICharactersService
{
    public PaginatedContainer<List<Character>> GetListByCreatorId(int creatorId, PaginateDataItemDto paginateDataItemDto) =>
        unitOfWork.CharactersRepository.GetPaginatedListByCreatorId(creatorId, paginateDataItemDto);

    public Character? GetById(int characterId) =>
        unitOfWork.CharactersRepository.GetById(characterId);
}