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
    public IActionResult CreateCharacter([FromBody] CreatingCharacterModel creatingCharacterModel)
    {
        //валидация
        if (string.IsNullOrWhiteSpace(creatingCharacterModel.Name))
            return BadRequest();

        int userId = HttpContext.GetAuthorizedUserId();
        var user = usersService.GetById(userId)!;

        var characterDto = creatingCharacterModel.MapToCharacterDto(user);

        var character = charactersService.CreateCharacter(characterDto);

        return Ok(character.CharacterId);
    }
}