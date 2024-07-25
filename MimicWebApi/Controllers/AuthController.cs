﻿using DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Models;
using MimicWebApi.Utils;
using Services;
using System.Security.Claims;
using DAL.EfClasses;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IConfiguration config, IUserService userService) : ControllerBase
{
    private string _clientLocation => config.GetValue<string>("ClientUrl")! + "/";

    [HttpGet("oidc/vk")]
    public Task VkAuth()
    {
        // todo: блядская ошибка с correlation cookie
        return HttpContext.ChallengeAsync("vk-oauth", new AuthenticationProperties()
        {
            RedirectUri = "oidc/checkUser"
        });
    }

    [Authorize]
    [HttpGet("oidc/checkUser")]
    public IActionResult CheckUser()
    {
        long oidcId = HttpContext.GetOidcUserId()!.Value;
        var user = userService.GetByOidcId(oidcId);

        if (user is not null) return SignInUser(user, new() { RedirectUri = _clientLocation });

        return Redirect(_clientLocation + "user/unbording");
    }

    [Authorize]
    [HttpPost("unbord")]
    public IActionResult Unbord([FromBody] UnbordingModel model)
    {
        var oidcId = HttpContext.GetOidcUserId()!.Value;
        var user = userService.GetByOidcId(oidcId);

        if (user is not null)
            return BadRequest($"User {user.Name} has already been unborded");

        var unbordedUser = model.ToUser();
        unbordedUser.OidcUserId = oidcId;

        userService.Add(unbordedUser);

        return SignInUser(unbordedUser);
    }

    private SignInResult SignInUser(User user, AuthenticationProperties? properties = null)
    {
        var claims = new List<Claim>()
        {
            new("user_id", user.UserId.ToString()),
            new("oidc_id", user.OidcUserId.ToString()!)
        };

        var identity = new ClaimsIdentity(claims, "auth-cookie");
        var principal = new ClaimsPrincipal(identity);
        return SignIn(principal, properties, "auth-cookie");
    }
}