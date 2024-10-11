using DAL;
using DAL.Dto;
using DAL.EfClasses;

namespace Services;

public interface ICharactersService
{
    PaginatedContainerDto<List<Character>> GetListByCreatorId(CharacterFilter filter);
    Character? GetById(int characterId, bool readOnly = true);
    Character CreateCharacter(Character character);
    void EditCharacter(Character original);
}

public class CharactersService(IUnitOfWork unitOfWork) : ICharactersService
{
    public PaginatedContainerDto<List<Character>> GetListByCreatorId(CharacterFilter filter) =>
        unitOfWork.CharactersRepository.GetPaginatedListByCreatorId(filter);

    public Character? GetById(int characterId, bool readOnly) =>
        unitOfWork.CharactersRepository.GetById(characterId, readOnly);

    public Character CreateCharacter(Character character)
    {
        character.CreateDate = DateTime.Now;
        character.Storage.Name ??= "Character Storage";

        unitOfWork.CharactersRepository.Insert(character);
        unitOfWork.Save();

        return character;
    }

    public void EditCharacter(Character original)
    {
        unitOfWork.CharactersRepository.Update(original);
        unitOfWork.Save();
    }
}