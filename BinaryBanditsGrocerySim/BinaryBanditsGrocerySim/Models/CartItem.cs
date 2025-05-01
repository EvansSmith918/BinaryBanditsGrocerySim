namespace BinaryBanditsGrocerySim.Models;

public class CartItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; } = 1;
    public double TotalCost => Price * Quantity;
}