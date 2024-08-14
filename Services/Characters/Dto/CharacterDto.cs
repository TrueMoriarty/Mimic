using DAL.EfClasses;
using Services.Items.Dto;

namespace Services.Characters.Dto;

public class CharacterDto
{
    public User Creator { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<ItemDto>? Items { get; set; }

    public Character MapToCharacter() => new()
    {
        CreatorId = Creator.UserId,
        Name = Name,
        Description = Description
    };
}