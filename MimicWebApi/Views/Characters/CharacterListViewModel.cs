using DAL.EfClasses;

namespace MimicWebApi.Views.Characters;

public class CharacterListViewModel
{
    public List<CharacterBaseViewModel> Characters { get; set; }

    public CharacterListViewModel(List<Character> characters)
    {
        Characters = characters.ConvertAll(c => new CharacterBaseViewModel(c));
    }
}