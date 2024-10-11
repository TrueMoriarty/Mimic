using DAL.EfClasses;
using MimicWebApi.Models.ItemModels;

namespace MimicWebApi.Models;

public class StorageModel : BaseModel
{
    public List<ItemModel> Items { get; set; }

    public Storage MapToStorage(int creatorId) => new()
    {
        Items = Items.ConvertAll(i => i.MapToItem(creatorId))
    };
}