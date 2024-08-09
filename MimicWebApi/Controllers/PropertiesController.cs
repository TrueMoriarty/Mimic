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
    [HttpPost]
    public IActionResult CreateItemProperty([FromBody] ItemPropertyModel propertyModel)
    {
        ItemPropertyDto propertyDto = propertyModel.MapToItemPropertyDto();

        ItemProperty property = 
            propertiesService.CreateItemProperty(propertyDto);

        return Ok(property);
    }
}