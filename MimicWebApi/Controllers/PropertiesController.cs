using DAL.EfClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Models;
using Services.Properties;
using Services.Properties.Dto;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PropertiesController(IPropertiesService propertiesService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetProperty()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateProperty([FromBody] PropertyModel propertyModel)
    {
        PropertyDto propertyDto = propertyModel.MapToPropertyDto();

        Property property = 
            propertiesService.CreateProperty(propertyDto);

        return Ok(property);
    }

    [HttpPatch]
    public IActionResult UpdateProperty()
    {
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteProperty()
    {
        return Ok();
    }

}