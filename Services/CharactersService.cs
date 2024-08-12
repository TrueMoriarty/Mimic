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
        unitOfWork.CharactersRepository
            .Get(
                character => character.CreatorId == creatorId,
                character => character.OrderBy(c => c.Name),
                "Room",
                true
            )
            .ToList();

    public Character? GetById(int characterId) =>
        unitOfWork.CharactersRepository
            .Get(
                character => character.CharacterId == characterId,
                includeProperties: "Room.RoomStorageRelations.Storage.Items.Properties",
                readOnly: true
            )
            .FirstOrDefault();
}