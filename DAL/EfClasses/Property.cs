namespace DAL.EfClasses;

public class Property
{
    public int PropertyId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    // --------------------------------------
    // Relationships

    public int ItemId { get; set; }
}
