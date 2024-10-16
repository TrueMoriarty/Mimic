using DAL.EfClasses;

namespace MimicWebApi.Views.Storages;

public class StorageViewModel
{
    public int StorageId { get; set; }
    public string? Description { get; set; }
    public string? Name { get; set; }
    public List<ItemViewModel> Items { get; set; }

    public StorageViewModel(Storage storage)
    {
        StorageId = storage.StorageId;
        Description = storage.Description;
        Name = storage.Name;
        Items = storage.Items?.Select(i => new ItemViewModel(i)).ToList();
    }
}