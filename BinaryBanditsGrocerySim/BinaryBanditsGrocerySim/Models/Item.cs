namespace BinaryBanditsGrocerySim.Models;

public class Item
{
    public int Id { get; set; }
    public required string Aisle { get; set; }
    public required string Name { get; set; }
    public required string UPC { get; set; }
    public double Price { get; set; }
    public double? DiscountPrice { get; set; }
    public int Quantity { get; set; }
}