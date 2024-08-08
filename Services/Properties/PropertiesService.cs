using DAL;
using DAL.EfClasses;
using Services.Properties.Dto;

namespace Services.Properties;

public interface IPropertiesService
{
    Property CreateProperty(PropertyDto propertyDto);
    List<Property> CreateBulkProperties(List<Property> properties);
}

public class PropertiesService(IUnitOfWork unitOfWork) : IPropertiesService
{
    public Property CreateProperty(PropertyDto propertyDto)
    {
        Property property = propertyDto.MapToPropety();

        unitOfWork.PropertiesRepository.Insert(property);
        unitOfWork.Save();

        return property;
    }

    public List<Property> CreateBulkProperties(List<Property> properties)
    {
        unitOfWork.PropertiesRepository.InsertRange(properties);
        unitOfWork.Save();

        return properties;
    }
}