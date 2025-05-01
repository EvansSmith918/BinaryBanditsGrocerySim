namespace BinaryBanditsGrocerySim;

using BinaryBanditsGrocerySim.Models;
using System.ComponentModel;

public partial class CartPage : ContentPage, INotifyPropertyChanged
{
    public bool HasItems => CartService.GetCartItems().Any();

    public CartPage()
    {
        InitializeComponent();
        BindingContext = this;
        CartService.LoadCart();
        UpdateUI();
    }

    private void UpdateUI()
    {
        CartItemsListView.ItemsSource = null;
        CartItemsListView.ItemsSource = CartService.GetCartItems();
        double totalPrice = CartService.GetCartItems().Sum(item => item.TotalCost);
        TotalPriceLabel.Text = $"Total: {totalPrice:C}";
        OnPropertyChanged(nameof(HasItems));
    }

    private async void OnIncreaseQuantityClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var cartItem = button?.CommandParameter as CartItem;
        if (cartItem != null)
        {
            var inventoryItem = InventoryService.GetItemById(cartItem.Id);
            if (inventoryItem != null && cartItem.Quantity + 1 > inventoryItem.Quantity)
            {
                await DisplayAlert("Error", "Not enough stock available.", "OK");
                return;
            }
            cartItem.Quantity++;
            CartService.SaveCart();
            UpdateUI();
        }
    }

    private void OnDecreaseQuantityClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var cartItem = button?.CommandParameter as CartItem;
        if (cartItem != null && cartItem.Quantity > 1)
        {
            cartItem.Quantity--;
            CartService.SaveCart();
            UpdateUI();
        }
    }

    private async void OnCheckoutClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CheckoutPage());
    }

    public new event PropertyChangedEventHandler? PropertyChanged;
    protected override void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}