using DAL.EfClasses;
using MimicWebApi.Views.Storages;

namespace MimicWebApi.Views.Characters;

public class CharacterViewModel : CharacterBaseViewModel
{
    public StorageViewModel Storage { get; set; }

    public CharacterViewModel(Character character) : base(character)
    {
        Storage = new StorageViewModel(character.Storage);
    }
}