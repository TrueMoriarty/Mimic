using DAL.Dto;
using DAL.Dto.ItemDto;
using DAL.EfClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Models.ItemModels;
using MimicWebApi.Utils;
using MimicWebApi.Views;
using Services.Items;
using Services.Items.Dto;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ItemsController(IItemsService itemsService) : ControllerBase
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

	[HttpGet("{itemId}")]
	public IActionResult GetItemById([FromRoute] int itemId)
	{
		Item? item = itemsService.GetItemById(itemId);

		if (item == null)
			return NotFound();

		ItemViewModel itemViewModel = new ItemViewModel(item);

		return Ok(itemViewModel);
	}

	[HttpPost]
	public IActionResult CreateItem([FromBody] ItemModel itemModel)
	{
		int? userId = HttpContext.GetUserId();

		if (userId == null)
			return Unauthorized();

		ItemDto itemDto = itemModel.MapToItemDto(userId.Value);

		Item item = itemsService.CreateItem(itemDto);

		return Ok(item.ItemId);
	}

	[HttpPut("{itemId}")]
	public IActionResult EditItem([FromRoute] int itemId,
		[FromBody] ItemModel changingItemModel)
	{
		int? userId = HttpContext.GetUserId();

		if (itemId == 0)
			return BadRequest();
		if (userId == null)
			return Unauthorized();

		Item? item = itemsService.GetLightItemById(itemId, userId.Value);

		if (item == null)
			return NotFound();

		ItemDto itemDto = changingItemModel.MapToItemDto(userId.Value);
		itemsService.EditItem(itemId, itemDto);

		return NoContent();
	}

	[HttpDelete("{itemId}")]
	public IActionResult DeleteItem([FromRoute] int itemId)
	{
		int? userId = HttpContext.GetUserId();

		if (itemId == 0)
			return BadRequest();
		if (userId == null)
			return Unauthorized();

		Item? item = itemsService.GetLightItemById(itemId, userId.Value);

		if (item == null)
			return NotFound();

		itemsService.DeleteItem(item);

		return NoContent();
	}
}