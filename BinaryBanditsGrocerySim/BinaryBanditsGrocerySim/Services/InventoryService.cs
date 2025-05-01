namespace BinaryBanditsGrocerySim;

using BinaryBanditsGrocerySim.Models;
using System.Text.Json;

public static class InventoryService
{
    private static List<Item> _items;

    static InventoryService()
    {
        LoadInventory();
        if (_items == null || !_items.Any())
        {
            _items = new List<Item>
            {
                new Item { Id = 1, Aisle = "CannedGoods", Name = "Canned Beans", UPC = "123456789012", Price = 1.29, DiscountPrice = 0.99, Quantity = 10 },
                new Item { Id = 2, Aisle = "CannedGoods", Name = "Canned Corn", UPC = "234567890123", Price = 1.49, Quantity = 15 },
                new Item { Id = 3, Aisle = "FreshItems", Name = "Apples", UPC = "345678901234", Price = 1.99, Quantity = 20 },
                new Item { Id = 4, Aisle = "Frozen", Name = "Frozen Pizza", UPC = "456789012345", Price = 5.99, DiscountPrice = 4.99, Quantity = 8 },
                new Item { Id = 5, Aisle = "Chilled", Name = "Milk", UPC = "567890123456", Price = 3.49, Quantity = 12 }
            };
            SaveInventory();
        }
    }

    private static void LoadInventory()
    {
        var json = Preferences.Get("Inventory", string.Empty);
        _items = string.IsNullOrEmpty(json) ? new List<Item>() : JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
    }

    private static void SaveInventory()
    {
        var json = JsonSerializer.Serialize(_items);
        Preferences.Set("Inventory", json);
    }

    public static List<Item> GetItems() => _items.ToList();

    public static Item GetItemById(int id) => _items.FirstOrDefault(i => i.Id == id) ?? throw new InvalidOperationException($"Item with ID {id} not found.");

    public static bool UpdateItem(Item item)
    {
        var existingItem = _items.FirstOrDefault(i => i.Id == item.Id);
        if (existingItem != null)
        {
            existingItem.Quantity = item.Quantity;
            SaveInventory();
            return true;
        }
        return false;
    }

    public static void AddItem(Item item)
    {
        _items.Add(item);
        SaveInventory();
    }

    public static void RemoveItems(List<CartItem> cartItems)
    {
        foreach (var cartItem in cartItems)
        {
            var inventoryItem = _items.FirstOrDefault(i => i.Id == cartItem.Id);
            if (inventoryItem != null)
            {
                inventoryItem.Quantity -= cartItem.Quantity;
            }
        }
        SaveInventory();
    }
}