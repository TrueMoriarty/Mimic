﻿using DAL;
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
        FillCharacterCovers(characters.Value);

        return characters;
    }

    public Character? GetById(int characterId, bool readOnly)
    {
        Character character = unitOfWork.CharactersRepository.GetById(characterId, readOnly);
        character.Cover = unitOfWork.AttachedFileRepository.GetFirstFileByOwner(character.CharacterId, AttachedFileOwnerType.Character);

        return character;
    }

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

    private void FillCharacterCovers(List<Character> characters)
    {
        var characterIds = characters.Select(u => u.CharacterId).ToArray();
        var attachedCovers = unitOfWork.AttachedFileRepository.GetFilesByOwner(characterIds, AttachedFileOwnerType.Character)
            .ToDictionary(x => x.OwnerId, x => x);

        var charactersWithCovers = characters.Where(character => attachedCovers.ContainsKey(character.CharacterId));
        foreach (Character character in charactersWithCovers)
            character.Cover = attachedCovers[character.CharacterId];
    }
}