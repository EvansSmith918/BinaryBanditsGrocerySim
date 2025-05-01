namespace BinaryBanditsGrocerySim;

using BinaryBanditsGrocerySim.Models;
using System.Text.Json;

public static class CartService
{
    private static List<CartItem> _cartItems = new List<CartItem>();

    static CartService()
    {
        LoadCart();
    }

    public static void LoadCart()
    {
        var json = Preferences.Get("Cart", string.Empty);
        _cartItems = string.IsNullOrEmpty(json) ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
    }

    public static void SaveCart()
    {
        var json = JsonSerializer.Serialize(_cartItems);
        Preferences.Set("Cart", json);
    }

    public static List<CartItem> GetCartItems() => _cartItems.ToList();

    public static void AddItem(Item item)
    {
        var cartItem = _cartItems.FirstOrDefault(ci => ci.Id == item.Id);
        if (cartItem != null)
        {
            cartItem.Quantity++;
        }
        else
        {
            _cartItems.Add(new CartItem
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.DiscountPrice ?? item.Price,
                Quantity = 1
            });
        }
        SaveCart();
    }

    public static void ClearCart()
    {
        _cartItems.Clear();
        SaveCart();
    }
}