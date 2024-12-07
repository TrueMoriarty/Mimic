using DAL;
using DAL.Dto;
using DAL.EfClasses;

namespace Services;

public interface ICharactersService
{
    PaginatedContainerDto<List<Character>> GetListByCreatorId(CharacterFilter filter);
    Character? GetById(int characterId, bool readOnly = true);
    void CreateCharacter(Character character);
    void EditCharacter(Character editedCharacter);
}

public class CharactersService(IUnitOfWork unitOfWork) : ICharactersService
{
    public PaginatedContainerDto<List<Character>> GetListByCreatorId(CharacterFilter filter)
    {
        var characters = unitOfWork.CharactersRepository.GetPaginatedListByCreatorId(filter);

        var characterIds = characters.Value.Select(u => u.CharacterId).ToArray();
        var attachedCovers = unitOfWork.AttachedFileRepository.GetFilesByOwner(characterIds, AttachedFileOwnerType.Character)
            .ToDictionary(x => x.OwnerId, x => x);

        foreach (var character in characters.Value)
        {
            if (attachedCovers.ContainsKey(character.CharacterId))
                character.Cover = attachedCovers[character.CharacterId];
        }

        return characters;
    }

    public Character? GetById(int characterId, bool readOnly) =>
        unitOfWork.CharactersRepository.GetById(characterId, readOnly);

    public void CreateCharacter(Character character)
    {
        character.CreateDate = DateTime.Now;
        character.Storage ??= new Storage();
        character.Storage.Name = "Character Storage";

        if (character.Storage.Items is not null)
            foreach (var item in character.Storage.Items)
                item.ItemId = 0;

        unitOfWork.CharactersRepository.Insert(character);
        unitOfWork.Save();
    }

    public void EditCharacter(Character editedCharacter)
    {
        unitOfWork.CharactersRepository.Update(editedCharacter);
        unitOfWork.Save();
    }
}