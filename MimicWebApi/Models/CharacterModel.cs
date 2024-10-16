using DAL.EfClasses;

namespace MimicWebApi.Models;

public class CharacterModel : BaseModel
{
    public StorageModel? Storage { get; set; }

    public Character MapToCharacter(int creatorId) => new()
    {
        Name = Name,
        Description = Description,
        CreatorId = creatorId,
        Storage = Storage?.MapToStorage(creatorId)
    };
}