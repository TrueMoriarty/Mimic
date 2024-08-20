using DAL.EfClasses;

namespace MimicWebApi.Views;

public class ItemViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ItemPropertyViewModel[] Properties { get; set; }

    public ItemViewModel(Item item)
    {
        Name = item.Name;
        Description = item.Description;
        Properties = item.Properties?.Select(p=> new ItemPropertyViewModel(p)).ToArray();
    }
}
