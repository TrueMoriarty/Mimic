using DAL.EfClasses;

namespace MimicWebApi.ViewModels;

public class ItemSuggestViewModel
{
    public int ItemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ItemSuggestViewModel(Item item)
    {
        ItemId = item.ItemId;
        Name = item.Name;
        Description = item.Description;
    }
}
