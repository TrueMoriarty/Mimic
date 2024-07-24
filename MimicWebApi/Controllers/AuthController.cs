using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IConfiguration config) : ControllerBase
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

        string clientLocation = config.GetValue<string>("ClientUrl")!;
        HttpContext.Response.Redirect(clientLocation + '/');
    }

    [Authorize]
    [HttpGet("oidc/userId")]
    public IActionResult GetUserId()
    {
        var OidcUserId = HttpContext.User.FindFirst("user_id")?.Value;

        return Ok(OidcUserId);
    }
}