using DAL;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] UserDTO user)
    {
        userService.Add(user.ToUser());
        return Ok();
    }

    [HttpGet]
    public IActionResult TestAuthWithUnBordered([FromBody] UserDTO user)
    {
        
        return Ok();
    }
}