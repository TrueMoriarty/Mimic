using DAL;
using DAL.EfClasses;
using Services.ItemProperties.Dto;

namespace Services.ItemProperties;

public interface IItemPropertiesService
{
    ItemProperty CreateItemProperty(ItemPropertyDto propertyDto);
    List<ItemProperty> GetAllItemProperties();
    List<ItemProperty> CreateBulkItemProperties(List<ItemProperty> properties);
}

public class ItemPropertiesService(IUnitOfWork unitOfWork) : IItemPropertiesService
{
    public ItemProperty CreateItemProperty(ItemPropertyDto propertyDto)
    {
        ItemProperty property = propertyDto.MapToPropety();

        unitOfWork.PropertiesRepository.Insert(property);
        unitOfWork.Save();

        return property;
    }

    public List<ItemProperty> GetAllItemProperties()
    {
        List<ItemProperty> properties = unitOfWork.PropertiesRepository.Get().ToList();
        return properties;
    }

    public List<ItemProperty> CreateBulkItemProperties(List<ItemProperty> properties)
    {
        unitOfWork.PropertiesRepository.InsertRange(properties);
        unitOfWork.Save();

        return properties;
    }
}