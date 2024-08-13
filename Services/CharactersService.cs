using DAL;
using DAL.EfClasses;

namespace Services;

public interface ICharactersService
{
    List<Character> GetListByCreatorId(int creatorId);
    Character? GetById(int characterId);
}

public class CharactersService(IUnitOfWork unitOfWork) : ICharactersService
{
    public List<Character> GetListByCreatorId(int creatorId) =>
        unitOfWork.CharactersRepository.GetListByCreatorId(creatorId);

    public Character? GetById(int characterId) =>
        unitOfWork.CharactersRepository.GetById(characterId);
}