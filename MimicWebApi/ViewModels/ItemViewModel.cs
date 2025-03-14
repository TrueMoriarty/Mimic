﻿using DAL.EfClasses;

namespace MimicWebApi.ViewModels;

public class ItemViewModel
{
    public int ItemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ItemPropertyViewModel[] Properties { get; set; }

    public ItemViewModel(Item item)
    {
        ItemId = item.ItemId;
        Name = item.Name;
        Description = item.Description;
        Properties = item.Properties?.Select(p => new ItemPropertyViewModel(p)).ToArray();
    }
}
