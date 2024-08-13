using DAL.Dto;
using DAL.EfClasses;
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
	public IActionResult CreateItem([FromBody] ItemModel itemModel)
	{
		var userId = HttpContext.GetUserId()!;

		var user = usersService.GetById(userId.Value);
		if (user == null)
			return NotFound();

		var itemDto = itemModel.MapToItemDto(user);

		var item = itemsService.CreateItem(itemDto);

		return Ok(item.ItemId);
	}

	[HttpGet]
	public IActionResult GetPagedItems([FromQuery] PaginateDataItemDto paginateDataItemDto)
	{
		List<Item> items = itemsService.GetPagedItems(paginateDataItemDto);

		return Ok(items);
	}

	[HttpDelete]
	public IActionResult DeleteItem([FromQuery] int itemId)
	{
		if (itemId == null)
		{
			return NotFound();
		}
		if (!itemsService.HasItemById(itemId))
		{
			return NotFound();	
		}

		itemsService.DeleteItem(itemId);
		
		return NoContent();
	}
}