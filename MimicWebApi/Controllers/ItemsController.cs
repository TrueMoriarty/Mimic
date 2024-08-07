using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Models;
using MimicWebApi.Utils;
using Services;
using Services.Items;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ItemsController(IItemsService itemsService, IUsersService usersService) : ControllerBase
{
    [HttpPost]
    public IActionResult CreateItem([FromBody] ItemModel model)
    {
        var userId = HttpContext.GetUserId()!;

        var user = usersService.GetById(userId.Value);
        if (user == null)
            return NotFound();

        var createItemDto = model.ToCreateItemDto(user);

        var item = itemsService.CreateItem(createItemDto);

        return Ok(item.ItemId);
    }
}