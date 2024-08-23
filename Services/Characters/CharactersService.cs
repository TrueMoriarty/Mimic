using DAL;
using DAL.Dto;
using DAL.EfClasses;
using Services.Characters.Dto;
using Services.Items;

namespace Services.Characters;

public interface ICharactersService
{
    PaginatedContainerDto<List<Character>> GetListByCreatorId(CharacterFilter filter);
    Character? GetById(int characterId);
    Character CreateCharacter(CharacterDto characterDto);
}

public class CharactersService(IUnitOfWork unitOfWork, IStoragesService storagesService, IItemsService itemsService) : ICharactersService
{
    public PaginatedContainerDto<List<Character>> GetListByCreatorId(CharacterFilter filter) =>
        unitOfWork.CharactersRepository.GetPaginatedListByCreatorId(filter);

    public Character? GetById(int characterId) =>
        unitOfWork.CharactersRepository.GetById(characterId);

    public Character CreateCharacter(CharacterDto characterDto)
    {
        var character = characterDto.MapToCharacter();

        character.CreateDate = DateTime.Now;

        character.Storage = new()
        {
            Name = $"{character.Name}'s Storage",
            Items = characterDto.Items?.ConvertAll(i => i.MapToItem())
        };

        unitOfWork.CharactersRepository.AddCharacter(character);
        unitOfWork.Save();

        return character;
    }
}