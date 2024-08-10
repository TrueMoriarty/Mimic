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
    public IActionResult GetCreatorUserCharactersList()
    {
        var userId = HttpContext.GetUserId();
        var characterList = charactersService.GetListByCreatorId(userId!.Value);
        var characterListViewModal = new CharacterListViewModel(characterList);

        return Ok(characterListViewModal);
    }
}