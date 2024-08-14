using DAL.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Models;
using MimicWebApi.Utils;
using MimicWebApi.Views.Characters;
using Services;
using Services.Characters;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CharactersController(ICharactersService charactersService, IUsersService usersService) : ControllerBase
{
    [HttpGet("creator")]
    public IActionResult GetCreatorUserCharactersList([FromQuery] PaginatedFilter paginatedFilter)
    {
        var userId = HttpContext.GetUserId();

        var characterList = charactersService.GetListByCreatorId(new CharacterFilter
        {
            CreatorId = userId!.Value,
            Pagination = paginatedFilter
        });

        var characterListViewModal = new PaginatedContainerDto<List<CharacterBaseViewModel>>(
            characterList.Value.ConvertAll(c => new CharacterBaseViewModel(c)),
            characterList.TotalCount,
            characterList.TotalPages
        );

        return Ok(characterListViewModal);
    }

    [HttpGet("{characterId}")]
    public IActionResult GetCreatorUserCharactersList([FromRoute] int characterId)
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

        int userId = HttpContext.GetUserId()!.Value;
        var user = usersService.GetById(userId)!;

        var characterDto = characterModel.MapToCharacterDto(user);

        var character = charactersService.CreateCharacter(characterDto);

        return Ok(character.CharacterId);
    }
}