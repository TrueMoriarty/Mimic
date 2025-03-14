﻿using DAL.Dto;
using DAL.EfClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Models;
using MimicWebApi.Utils;
using MimicWebApi.ViewModels.Characters;
using Services;
using System.Text.Json;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CharactersController(ICharactersService charactersService) : ControllerBase
{
    [HttpGet("page")]
    public IActionResult GetCreatorUserCharactersList([FromQuery] CharacterFilter paginatedFilter)
    {
        var characterList = charactersService.GetListByCreatorId(paginatedFilter);

        var characterListViewModal = new PaginatedContainerDto<List<CharacterBaseViewModel>>(
            characterList.Value.ConvertAll(c => new CharacterBaseViewModel(c)),
            characterList.TotalCount,
            characterList.TotalPages
        );

        return Ok(characterListViewModal);
    }

    [HttpGet("suggests")]
    public IActionResult GetCreatorUserCharactersList([FromQuery] string query)
    {
        var characters = charactersService.GetSuggestedCharacters(query, HttpContext.GetAuthorizedUserId());

        var charactersViewModel = characters.Select(c =>
            new CharacterBaseViewModel(c)
        ).ToArray();

        return Ok(charactersViewModel);
    }

    [HttpGet("{characterId}")]
    public IActionResult GetCharacter([FromRoute] int characterId)
    {
        var character = charactersService.GetById(characterId, includeAttachedFiles: true);

        return character is null ? NotFound() : Ok(new CharacterViewModel(character));
    }

    [HttpPost]
    public IActionResult CreateCharacter([FromForm] string characterModelJson, [FromForm] IFormFile? cover)
    {
        CharacterModel? characterModel = JsonSerializer.Deserialize<CharacterModel>(characterModelJson,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        //валидация
        if (characterModel is null || string.IsNullOrWhiteSpace(characterModel.Name))
            return BadRequest();

        int userId = HttpContext.GetAuthorizedUserId();
        Character character = characterModel.MapToCharacter(userId);
        character.Cover = cover?.MapToAttachedFile();

        charactersService.CreateCharacter(character);

        return Ok(new CharacterViewModel(character));
    }

    [HttpPut("{characterId}")]
    public IActionResult UpdateCharacter([FromRoute] int characterId, [FromForm] string characterModelJson, [FromForm] IFormFile? cover)
    {
        int userId = HttpContext.GetAuthorizedUserId();
        CharacterModel? characterModel = JsonSerializer.Deserialize<CharacterModel>(characterModelJson,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        var original = charactersService.GetById(characterId, includeAttachedFiles: true);
        if (original is null)
            return NotFound();

        Character character = characterModel.MapToCharacter(userId);
        character.CharacterId = characterId;
        character.CreateDate = original.CreateDate;

        if (cover is not null)
        {
            if (original.Cover is not null)
            {
                character.Cover = original.Cover;
                character.Cover.Stream = new MemoryStream();
                cover.CopyTo(character.Cover.Stream);
            }
            else
                character.Cover = cover.MapToAttachedFile(characterId, AttachedFileOwnerType.Character);
        }

        charactersService.EditCharacter(character);

        return NoContent();
    }

    [HttpDelete("{characterId}")]
    public IActionResult DeleteCharacter([FromRoute] int characterId)
    {
        Character? character = charactersService.GetById(characterId, readOnly: false, includeAttachedFiles: true);
        if (character is null)
            return NotFound("Character not found");

        int userId = HttpContext.GetAuthorizedUserId();
        if (character.CreatorId != userId)
            return Forbid();

        charactersService.DeleteCharacter(character);

        return NoContent();
    }
}