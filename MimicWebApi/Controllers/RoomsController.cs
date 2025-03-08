using DAL.Dto;
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

    [HttpGet("page")]
    public IActionResult GetPaginatedRooms([FromQuery] RoomsFilter filter)
    {
        filter.UserId = HttpContext.GetAuthorizedUserId();

        var rooms = roomService.GetPaginatedRooms(filter);

        var roomsViewModal = new PaginatedContainerDto<List<RoomViewModel>>(
            rooms.Value.ConvertAll(c => new RoomViewModel(c)),
            rooms.TotalCount,
            rooms.TotalPages
        );

        return Ok(roomsViewModal);
    }

    [HttpPost("{roomId}/join")]
    public IActionResult JoinRoom([FromRoute] int roomId, [FromForm] int characterId)
    {
        Room? room = roomService.GetRoomById(roomId);
        if (room is null) return NotFound("Room not found");

        Character? character = charactersService.GetById(characterId);
        if (character is null) return NotFound("Character not found");

        roomService.JoinRoom(room, character);
        return Ok();
    }
}