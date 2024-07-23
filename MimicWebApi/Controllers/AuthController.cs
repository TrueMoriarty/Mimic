﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpGet("oidc/vk")]
    public Task VkAuth()
    {
        return HttpContext.ChallengeAsync("vk-oauth", new AuthenticationProperties()
        {
            RedirectUri = "oidc/checkUser"
        });
    }

    [HttpGet("oidc/checkUser")]
    public void CheckUser()
    {
        var user = HttpContext.User.FindFirst("user_id")?.Value;

        //TODO: посдтавить нормальный юрл из конфига 
        HttpContext.Response.Redirect("https://localhost:5173/");
    }
}