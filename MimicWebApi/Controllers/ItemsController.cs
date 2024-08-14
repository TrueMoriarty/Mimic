using DAL.Dto;
using DAL.Dto.ItemDto;
using DAL.EfClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Models.ItemModels;
using MimicWebApi.Utils;
using Services;
using Services.Items;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ItemsController(IItemsService itemsService, IUsersService usersService) : ControllerBase
{
	// TODO: Добавить проверку на UserId [FromQuery]
	[HttpGet]
	public IActionResult GetPagedItems([FromQuery] PaginateDataItemDto paginateDataItemDto)
	{
		List<Item> items = itemsService.GetPagedItems(paginateDataItemDto);

		return Ok(items);
	}

	[HttpPost]
	public IActionResult CreateItem([FromBody] PostItemModel itemModel)
	{
		var userId = HttpContext.GetUserId()!;

		var user = usersService.GetById(userId.Value);
		if (user == null)
			return NotFound();

		var itemDto = itemModel.MapToPostItemDto(user);

		var item = itemsService.CreateItem(itemDto);

		return Ok(item.ItemId);
	}

	// TODO: добавить на проверку CreatorId
	//[HttpPatch]

	// TODO: добавить проверку CreatorId, изменить на TryDeleteItem, а в нем возвращать bool при null
	[HttpDelete("{itemId}")]
	public IActionResult TryDeleteItem([FromRoute] int itemId)
	{
		int? creatorId = HttpContext.GetUserId();
		if (itemId == 0)
		{
			return BadRequest();
		}
		if (creatorId == null)
		{
			return BadRequest();
		}

		Item? item = itemsService.TryDeleteItem(itemId, creatorId);

		if (item == null)
		{
			return NotFound();
		}

		return NoContent();
	}
}