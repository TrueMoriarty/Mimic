using DAL.Dto;
using DAL.Dto.ItemDto;
using DAL.EfClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Models.ItemModels;
using MimicWebApi.Utils;
using MimicWebApi.Views;
using Services;
using Services.Items;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ItemsController(IItemsService itemsService, IUsersService usersService) : ControllerBase
{
	[HttpGet]
	public IActionResult GetPaginatedItems([FromQuery] ItemFilter paginateDataItemDto)
	{
		PaginatedContainerDto<List<Item>> itemsList = 
			itemsService.GetPaginatedItems(paginateDataItemDto);

		var itemsListViewModel = 
			new PaginatedContainerDto<List<ItemViewModel>>
			(
				itemsList.Value.ConvertAll(item => new ItemViewModel(item)),
				itemsList.TotalCount,
				itemsList.TotalPages
			);

		return Ok(itemsListViewModel);
	}

	[HttpPost]
	public IActionResult CreateItem([FromBody] CreateItemModel itemModel)
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

	[HttpDelete("{itemId}")]
	public IActionResult TryDeleteItem([FromRoute] int itemId)
	{
		int? creatorId = HttpContext.GetUserId();
		if (itemId == 0 || creatorId == null)
		{
			return BadRequest();
		}

		Item? item = itemsService.TryDeleteItem(itemId, creatorId);
		
		return item is null ? NotFound() : NoContent();
	}
}