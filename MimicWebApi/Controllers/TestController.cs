using DAL;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController(IUsersService usersService) : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] UserDTO user)
    {
        usersService.Add(user.ToUser());
        return Ok();
    }

    [HttpPost("testLogin")]
    public IActionResult TestUserLogin([FromQuery] int id)
    {
        var identity = new ClaimsIdentity("auth-cookie");

        identity.AddClaim(new Claim("user_id", id.ToString()));

        var principal = new ClaimsPrincipal(identity);
        return SignIn(principal, "auth-cookie");
    }
}