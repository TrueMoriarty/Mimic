using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Models;
using Services;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoragesController(IStoragesService storagesService) : ControllerBase
{
    [HttpPost]
    public IActionResult CreateStorage([FromBody] StorageModel model)
    {
        var storage = storagesService.CreateStorage(model.Name, model.Description);
        return Ok(storage.StorageId);
    }

    [HttpPut]
    public IActionResult PutItem([FromBody] PutItemInStorageModel model)
    {
        storagesService.PutItem(model.StorageId, model.ItemId);
        return Ok();
    }

    [HttpGet("{id}/items")]
    public IActionResult GetItems([FromRoute] int id)
    {
        var items = storagesService.GetItems(id);

        return Ok(items.Select(item => new { Id = item.ItemId, item.Name }));
    }
}