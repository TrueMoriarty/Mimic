using DAL.EfClasses;
using System.Text.Json.Serialization;

namespace MimicWebApi.Models;

public class UnbordingModel
{
    [JsonPropertyName("username")]
    public string Username { get; set; }

    public User ToUser() => new() { Name = Username };
}