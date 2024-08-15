using DAL.EfClasses;
using MimicWebApi.Models.ItemModels;
using Services.Characters.Dto;

namespace MimicWebApi.Models;

public class CreatingCharacterModel : BaseModel
{
    public List<CreateItemModel> Items { get; set; }

    public CharacterDto MapToCharacterDto(User creator) => new()
    {
        Creator = creator,
        Name = Name,
        Description = Description,
        Items = Items?.ConvertAll(i => i.MapToCreatingItemDto(creator)),
    };
}