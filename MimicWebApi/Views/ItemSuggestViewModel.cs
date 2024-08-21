using DAL.EfClasses;

namespace MimicWebApi.Views;

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
