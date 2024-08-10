using DAL;
using DAL.EfClasses;

namespace Services;

public interface ICharactersService
{
    List<Character> GetListByCreatorId(int creatorId);
}

public class CharactersService(IUnitOfWork unitOfWork) : ICharactersService
{
    public List<Character> GetListByCreatorId(int creatorId) =>
        unitOfWork.CharactersRepository
            .Get(
                character => character.CreatorId == creatorId,
                character => character.OrderBy(c => c.Name),
                "Room"
            )
            .ToList();
}