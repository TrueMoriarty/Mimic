using DAL.EfClasses;
using MimicWebApi.Models.ItemModels;

namespace MimicWebApi.Models;

public class StorageModel : BaseModel
{
    public int? StorageId { get; set; }
    public List<ItemModel> Items { get; set; }

    public Storage MapToStorage(int creatorId) => new()
    {
        StorageId = StorageId ?? 0,
        Items = Items.ConvertAll(i => i.MapToItem(creatorId))
    };
}