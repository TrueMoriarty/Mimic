﻿using DAL.Dto;
using DAL.EfClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Models;
using MimicWebApi.Utils;
using MimicWebApi.Views.Characters;
using Services;

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

    [HttpGet("{characterId}")]
    public IActionResult GetCharacter([FromRoute] int characterId)
    {
        var character = charactersService.GetById(characterId);

        return character is null ? NotFound() : Ok(new CharacterViewModel(character));
    }

    [HttpPost]
    public IActionResult CreateCharacter([FromBody] CharacterModel characterModel)
    {
        //валидация
        if (string.IsNullOrWhiteSpace(characterModel.Name))
            return BadRequest();

        int userId = HttpContext.GetAuthorizedUserId();

        Character character = characterModel.MapToCharacter(userId);
        int characterId = charactersService.CreateCharacter(character).CharacterId;

        return Ok(characterId);
    }

    [HttpPut("{characterId}")]
    public IActionResult UpdateCharacter([FromRoute] int characterId, [FromBody] CharacterModel characterModel)
    {
        int userId = HttpContext.GetAuthorizedUserId();

        var original = charactersService.GetById(characterId, true);
        if (original is null)
            return NotFound();

        Character character = characterModel.MapToCharacter(userId);
        character.CharacterId = characterId;
        character.CreateDate = original.CreateDate;

        charactersService.EditCharacter(character);

        return NoContent();
    }
}