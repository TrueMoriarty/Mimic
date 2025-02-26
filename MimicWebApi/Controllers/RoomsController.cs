using DAL.EfClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Utils;
using MimicWebApi.ViewModels;
using Services;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RoomsController(IRoomService roomService, ICharactersService charactersService) : ControllerBase
{
    [HttpPost]
    public IActionResult AddRoom([FromForm] string name)
    {
        Room room = roomService.CreateRoom(HttpContext.GetAuthorizedUserId(), name);
        return Ok(room.RoomId);
    }

    [HttpGet]
    public IActionResult GetRoom([FromQuery] int masterId)
    {
        RoomViewModel[] rooms = roomService.GetRoomsByMasterId(masterId)
            .Select(r=> new RoomViewModel(r)).ToArray();
        return Ok(rooms);
    }

    [HttpPost("{roomId}/join")]
    public IActionResult JoinRoom([FromRoute] int roomId, [FromForm] int characterId)
    {
        Room? room = roomService.GetRoomById(roomId);
        Character? character = charactersService.GetById(characterId);

        roomService.JoinRoom(room, character);
        return Ok();
    }
}