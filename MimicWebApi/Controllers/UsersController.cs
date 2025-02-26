using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Utils;
using MimicWebApi.ViewModels;
using Services;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUsersService usersService) : ControllerBase
{
    [Authorize]
    [HttpGet("info")]
    public IActionResult GetUserInfo()
    {
        int userId = HttpContext.GetAuthorizedUserId();

        var user = usersService.GetById(userId);
        if (user == null) return NotFound();

        return Ok(new UserInfoViewModel(user!));
    }
}