namespace BinaryBanditsGrocerySim;

using BinaryBanditsGrocerySim.Models;

public partial class InventoryManagerPage : ContentPage
{
    public InventoryManagerPage()
    {
        InitializeComponent();
        LoadInventory();
    }

    private void LoadInventory()
    {
        InventoryListView.ItemsSource = InventoryService.GetItems();
    }

    private async void OnUpdateQuantityClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var item = button?.CommandParameter as Item;
        if (item != null)
        {
            if (item.Quantity < 0)
            {
                await DisplayAlert("Error", "Quantity cannot be negative.", "OK");
                LoadInventory();
                return;
            }
            bool success = InventoryService.UpdateItem(item);
            if (success)
            {
                await DisplayAlert("Success", $"Updated quantity for {item.Name} to {item.Quantity}", "OK");
                LoadInventory();
            }
            else
            {
                await DisplayAlert("Error", "Failed to update item quantity.", "OK");
            }
        }
    }

    private async void OnBackToHomeClicked(object sender, EventArgs e)
    {
        try
        {
            await DisplayAlert("Debug", "Navigating to HomePage...", "OK");
            await Shell.Current.GoToAsync("//HomePage");
            await DisplayAlert("Debug", "Navigation to HomePage completed.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Navigation Error", $"Failed to navigate to HomePage: {ex.Message}", "OK");
        }
    }
}