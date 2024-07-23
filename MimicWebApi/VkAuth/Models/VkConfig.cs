namespace MimicWebApi.VkAuth.Models;

public class VkConfig
{
    public string? ClientId { get; set; }
    public string? CodeChallengeMethod { get; set; }
    public string? CodeVerifier { get; set; }
}