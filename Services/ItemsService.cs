using DAL;
using DAL.EfClasses;

namespace Services;

public interface IItemsService
{
    Item CreateItem(User creator, string name, string description, int? storageId = null);
}

public class ItemsService(IUnitOfWork unitOfWork) : IItemsService
{
    public Item CreateItem(User creator, string name, string description, int? storageId = null)
    {
        var item = new Item
        {
            Name = name,
            Description = description,

            CreatorId = creator.UserId,
            StorageId = storageId,
        };

        unitOfWork.ItemRepository.Insert(item);
        unitOfWork.Save();

        return item;
    }
}