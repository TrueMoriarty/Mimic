using Microsoft.AspNetCore.Mvc;
using Services;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertiesController(IPropertiesService propertiesService) : ControllerBase
{
    [HttpPost]
    public IActionResult CreateProperty()
    {
        return Ok();
    }
}