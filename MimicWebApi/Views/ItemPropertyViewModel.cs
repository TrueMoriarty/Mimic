using DAL.EfClasses;

namespace MimicWebApi.Views;

public class ItemPropertyViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ItemPropertyViewModel(ItemProperty itemProperty)
    {
        Name = itemProperty.Name;
        Description = itemProperty.Description;
    }
}