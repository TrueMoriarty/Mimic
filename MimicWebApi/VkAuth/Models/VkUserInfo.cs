using System.Text.Json.Serialization;

namespace MimicWebApi.VkAuth.Models;

public class VkUserInfo
{
    [JsonPropertyName("user")]
    public VkUserInfoContainer User { get; set; }
}

public class VkUserInfoContainer
{
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }
}