using DAL.EfClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Models;
using Services.ItemProperties;
using Services.ItemProperties.Dto;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ItemPropertiesController(IItemPropertiesService propertiesService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllItemProperty()
    {
        List<ItemProperty> properties = propertiesService.GetAllItemProperties();

        return Ok(properties);
    }

    [HttpPost]
    public IActionResult CreateItemProperty([FromBody] ItemPropertyModel propertyModel)
    {
        ItemPropertyDto propertyDto = propertyModel.MapToItemPropertyDto();

        ItemProperty property = 
            propertiesService.CreateItemProperty(propertyDto);

        return Ok(property);
    }

    [HttpPatch]
    public IActionResult UpdateItemProperty()
    {
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteItemProperty()
    {
        return Ok();
    }

}