using DAL.EfClasses;
using DAL.EfCode;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories;

internal class CharacterRepository(MimicContext context) : GenericRepository<Character>(context), ICharacterRepository
{
    public List<Character> GetListByCreatorId(int creatorId) =>
        Get(
                character => character.CreatorId == creatorId,
                character => character.OrderBy(c => c.Name),
                "Room",
                true
            )
            .ToList();

    public Character GetById(int id) =>
        Get(
                character => character.CharacterId == id,
                includeProperties: "Room,Storage.Items.Properties",
                readOnly: true
            )
            .FirstOrDefault();
}