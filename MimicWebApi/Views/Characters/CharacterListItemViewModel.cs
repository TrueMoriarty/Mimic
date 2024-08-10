using DAL.EfClasses;

namespace MimicWebApi.Views.Characters;

public class CharacterListItemViewModel
{
    public int CharacterId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public string RoomName { get; set; }

    public CharacterListItemViewModel(Character character)
    {
        CharacterId = character.CharacterId;
        Name = character.Name!;
        Description = character.Description!;
        RoomName = character.Room?.Name!;
    }
}