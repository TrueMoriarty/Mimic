using DAL;
using DAL.EfClasses;

namespace Services;

public interface IPropertiesService
{
    Property CreateProperty(string name, string description, int itemId);
    List<Property> CreateBulkProperties(List<Property> properties);
}

public class PropertiesService(IUnitOfWork unitOfWork) : IPropertiesService
{
    public Property CreateProperty(string name, string description, int itemId)
    {
        var prop = new Property() { Name = name, Description = description, ItemId = itemId };

        unitOfWork.PropertiesRepository.Insert(prop);
        unitOfWork.Save();

        return prop;
    }

    public List<Property> CreateBulkProperties(List<Property> properties)
    {
        unitOfWork.PropertiesRepository.InsertRange(properties);
        unitOfWork.Save();

        return properties;
    }
}