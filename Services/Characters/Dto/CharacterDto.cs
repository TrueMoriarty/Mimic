using DAL.EfClasses;
using Services.Items.Dto;

namespace Services.Characters.Dto;

public class CharacterDto
{
    public int CreatorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<ItemDto>? Items { get; set; }

    public Character MapToCharacter() => new()
    {
        CreatorId = CreatorId,
        Name = Name,
        Description = Description, 
    };
}