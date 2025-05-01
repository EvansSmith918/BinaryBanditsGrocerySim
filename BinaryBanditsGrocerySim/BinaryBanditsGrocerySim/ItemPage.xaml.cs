namespace BinaryBanditsGrocerySim;

using BinaryBanditsGrocerySim.Models;

public partial class ItemPage : ContentPage
{
    private string _aisle;

    public ItemPage(string aisle)
    {
        InitializeComponent();
        _aisle = aisle;
        AisleLabel.Text = aisle;
        LoadItems();
    }

    private void LoadItems()
    {
        var items = InventoryService.GetItems().Where(item => item.Aisle == _aisle).ToList();
        ItemListView.ItemsSource = items;
    }

    private async void OnAddToCartClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var item = button?.CommandParameter as Item;
        if (item != null)
        {
            if (item.Quantity <= 0)
            {
                await DisplayAlert("Error", "Item is out of stock.", "OK");
                return;
            }
            CartService.AddItem(item);
            await DisplayAlert("Success", $"{item.Name} added to cart!", "OK");
        }
    }
}