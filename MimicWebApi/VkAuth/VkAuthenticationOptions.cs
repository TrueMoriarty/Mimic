using Microsoft.AspNetCore.Authentication.OAuth;

namespace MimicWebApi.VkAuth;

public class VkAuthenticationOptions : OAuthOptions
{
    public string? CodeVerifier { get; set; }
    public string? CodeChallengeMethod { get; set; }
}