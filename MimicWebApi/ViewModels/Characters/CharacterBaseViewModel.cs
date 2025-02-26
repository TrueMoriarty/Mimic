using DAL.EfClasses;

namespace MimicWebApi.ViewModels.Characters;

public class CharacterBaseViewModel
{
    public int CharacterId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string RoomName { get; set; }
    public string? CoverUrl { get; set; }

    public CharacterBaseViewModel(Character character)
    {
        CharacterId = character.CharacterId;
        Name = character.Name!;
        Description = character.Description!;
        RoomName = character.Room?.Name!;
        CoverUrl = character.Cover?.Url;
    }
}