﻿using DAL.EfClasses;

namespace MimicWebApi.Views.Characters;

public class CharacterListViewModel
{
    public List<CharacterListItemViewModel> Characters { get; set; }

    public CharacterListViewModel(List<Character> characters)
    {
        Characters = characters.ConvertAll(c => new CharacterListItemViewModel(c));
    }
}