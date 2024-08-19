using MimicWebApi.Models.ItemModels;
using Services.Characters.Dto;

namespace MimicWebApi.Models;

public class CreatingCharacterModel : BaseModel
{
    public List<ItemModel>? Items { get; set; }

    public CharacterDto MapToCharacterDto(int creatorId) => new()
    {
        CreatorId = creatorId,
        Name = Name,
        Description = Description,
        Items = Items?.ConvertAll(i => i.MapToItemDto(creatorId))
    };
}