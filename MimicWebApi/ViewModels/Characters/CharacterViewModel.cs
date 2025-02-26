using DAL.EfClasses;
using MimicWebApi.ViewModels.Storages;

namespace MimicWebApi.ViewModels.Characters;

public class CharacterViewModel : CharacterBaseViewModel
{
    public StorageViewModel Storage { get; set; }

    public CharacterViewModel(Character character) : base(character)
    {
        Storage = new StorageViewModel(character.Storage);
    }
}