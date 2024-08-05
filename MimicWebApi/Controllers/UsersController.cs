﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Utils;
using MimicWebApi.Views;
using Services;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [Authorize]
    [HttpGet("info")]
    public IActionResult GetUserInfo()
    {
        long? userId = HttpContext.GetUserId();
        if (userId == null) return BadRequest();

        var user = userService.GetById((int) userId.Value);
        if (user == null) return NotFound();

        return Ok(new UserInfoViewModel(user!));
    }
}