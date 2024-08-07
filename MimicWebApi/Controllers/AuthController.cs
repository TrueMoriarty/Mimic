using DAL.EfClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Models;
using MimicWebApi.Utils;
using Services;
using System.Security.Claims;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IUsersService usersService) : ControllerBase
{
    [HttpGet("oidc/vk")]
    public ChallengeResult VkAuth() => Challenge("vk-oauth");

    [Authorize]
    [HttpPost("unbord")]
    public IActionResult Unbord([FromBody] UnbordingModel model)
    {
        var externalId = HttpContext.GetExternalUserId()!;
        var user = usersService.GetByExternalId(externalId);

        if (user is not null)
            return BadRequest($"User {user.Name} has already been unborded");

        var unbordedUser = model.ToUser();
        unbordedUser.ExternalUserId = externalId;

        usersService.Add(unbordedUser);

        return SignInUser(unbordedUser);
    }

    private SignInResult SignInUser(User user, AuthenticationProperties? properties = null)
    {
        var claims = new List<Claim>()
        {
            new("user_id", user.UserId.ToString()),
        };

        var identity = new ClaimsIdentity(claims, "auth-cookie");
        var principal = new ClaimsPrincipal(identity);
        return SignIn(principal, properties, "auth-cookie");
    }
}