namespace DAL.EfClasses;

public class Properties
{
    public int PropertiesId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    // --------------------------------------
    // Relationships

    public int ItemId { get; set; }
}
