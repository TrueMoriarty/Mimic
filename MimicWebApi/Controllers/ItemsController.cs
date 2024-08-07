using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Models;
using MimicWebApi.Utils;
using Services;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController(IItemsService itemsService, IUsersService usersService) : ControllerBase
{
    [HttpPost]
    public IActionResult CreateItem([FromBody] CreateItemModel model)
    {
        var userId = HttpContext.GetUserId();
        if (userId is null)
            return BadRequest();

        var user = usersService.GetById(userId.Value);

        var item = itemsService.CreateItem(user, model.Name, model.Description, model.StorageId);

        return Ok(item.ItemId);
    }
}