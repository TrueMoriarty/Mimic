using Services.Items.Dto;

namespace Services.Characters.Dto;

public class CharacterChangesDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<ItemDto>? Items { get; set; }
}