using DAL;
using DAL.EfClasses;

namespace Services;

public interface IStoragesService
{
    Storage CreateStorage(string name, string description);
    void PutItem(int storageId, int itemId);
    List<Item> GetItems(int storageId);
}

public class StoragesService(IUnitOfWork unitOfWork) : IStoragesService
{
    public Storage CreateStorage(string name, string description)
    {
        var storage = new Storage() { Name = name, Description = description };

        unitOfWork.StorageRepository.Insert(storage);
        unitOfWork.Save();

        return storage;
    }

    public void PutItem(int storageId, int itemId)
    {
        var item = unitOfWork.ItemRepository.GetByID(itemId);

        item.StorageId = storageId;
        unitOfWork.Save();
    }

    public List<Item> GetItems(int storageId)
    {
        return unitOfWork.ItemRepository.Get(item => item.StorageId == storageId).ToList();
    }
}