using DAL.EfClasses;
using Services.Items.Dto;

namespace MimicWebApi.Models;

public class ItemModel : BaseModel
{
    public int? StorageId { get; set; }

    public List<PropertyModel>? Properties { get; set; }

    public CreateItemDto ToCreateItemDto(User creator)
    {
        var itemDto = new CreateItemDto
        {
            Creator = creator,
            StorageId = StorageId,
            Name = Name,
            Description = Description,
            Properties = Properties?.ConvertAll(p => p.ToProperty())
        };

        return itemDto;
    }
}