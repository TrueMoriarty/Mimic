﻿using DAL.EfClasses;

namespace MimicWebApi.Views.Characters;

public class CharacterViewModel : CharacterBaseViewModel
{
    public ItemViewModel[] Items { get; set; }

    public CharacterViewModel(Character character) : base(character)
    {
        Items = character.Storage?.Items?
            .Select(i => new ItemViewModel(i)).ToArray();
    }
}