using DAL.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Utils;
using MimicWebApi.Views.Characters;
using Services;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CharactersController(ICharactersService charactersService) : ControllerBase
{
    [HttpGet("creator")]
    public IActionResult GetCreatorUserCharactersList([FromQuery] PaginatedFilter paginatedFilter)
    {
        var userId = HttpContext.GetUserId();

        var characterList = charactersService.GetListByCreatorId(new CharacterFilter
        {
            CreatorId = userId!.Value,
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
}