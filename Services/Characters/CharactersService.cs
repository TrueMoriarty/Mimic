using DAL;
using DAL.Dto;
using DAL.EfClasses;
using Services.Characters.Dto;

namespace Services.Characters;

public interface ICharactersService
{
    PaginatedContainerDto<List<Character>> GetListByCreatorId(CharacterFilter filter);
    Character? GetById(int characterId, bool readOnly = true);
    Character CreateCharacter(CharacterDto characterDto);
    void EditCharacter(Character original, CharacterChangesDto changes);
}

public class CharactersService(IUnitOfWork unitOfWork) : ICharactersService
{
    public PaginatedContainerDto<List<Character>> GetListByCreatorId(CharacterFilter filter) =>
        unitOfWork.CharactersRepository.GetPaginatedListByCreatorId(filter);

    public Character? GetById(int characterId, bool readOnly) =>
        unitOfWork.CharactersRepository.GetById(characterId, readOnly);

    public Character CreateCharacter(CharacterDto characterDto)
    {
        var character = characterDto.MapToCharacter();

        character.CreateDate = DateTime.Now;

        character.Storage = new()
        {
            Name = "Character Storage",
            Items = characterDto.Items?.ConvertAll(i => i.MapToItem())
        };

        unitOfWork.CharactersRepository.Insert(character);
        unitOfWork.Save();

        return character;
    }

    public void EditCharacter(Character original, CharacterChangesDto changes)
    {
        Character newCharacter = new Character()
        {
            CharacterId = original.CharacterId,
            CreatorId = original.CreatorId,
            CreateDate = original.CreateDate,

            Name = original.Name,
            Description = original.Description,
            Storage = original.Storage,
        };

        if (!string.IsNullOrWhiteSpace(changes.Name))
            newCharacter.Name = changes.Name;

        if (changes.Description is not null)
            newCharacter.Description = changes.Description;

        if (changes.Items is not null)
        {
            // TODO: это кал
            

        }

        unitOfWork.CharactersRepository.Update(newCharacter);
        unitOfWork.Save();
    }
}