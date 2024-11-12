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
    public PaginatedContainerDto<List<Character>> GetListByCreatorId(CharacterFilter filter) =>
        unitOfWork.CharactersRepository.GetPaginatedListByCreatorId(filter);

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