using DAL.EfClasses;
using Services.Characters.Dto;

namespace MimicWebApi.Models;

public class CharacterModel : BaseModel
{
    public List<ItemModel> Items { get; set; }

    public CharacterDto MapToCharacterDto(User creator) => new()
    {
        Creator = creator,
        Name = Name,
        Description = Description,
        Items = Items?.ConvertAll(i => i.MapToItemDto(creator)),
    };
}