using DAL.EfClasses;

namespace MimicWebApi.Models;

public class PropertyModel : BaseModel
{
    public Property ToProperty() => new()
    {
        Name = Name,
        Description = Description,
    };
}