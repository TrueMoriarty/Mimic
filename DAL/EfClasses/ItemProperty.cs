namespace DAL.EfClasses;

public class ItemProperty
{
    public int ItemPropertyId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    // --------------------------------------
    // Relationships

    public int ItemId { get; set; }
}
