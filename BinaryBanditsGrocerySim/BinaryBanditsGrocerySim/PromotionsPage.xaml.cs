namespace BinaryBanditsGrocerySim;

using BinaryBanditsGrocerySim.Models;

public partial class PromotionsPage : ContentPage
{
    public PromotionsPage()
    {
        InitializeComponent();
        LoadPromotions();
    }

    private void LoadPromotions()
    {
        var promotionalItems = InventoryService.GetItems()
            .Where(item => item.DiscountPrice.HasValue)
            .ToList();
        PromotionsListView.ItemsSource = promotionalItems;
    }
    //
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

    private async void OnBackToHomeClicked(object sender, EventArgs e) => await Navigation.PopAsync();
}
