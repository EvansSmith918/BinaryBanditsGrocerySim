namespace BinaryBanditsGrocerySim; 

public partial class AislePage : ContentPage
{
    public AislePage()
    {
        InitializeComponent();
        LoadAisles();
    }
//Load Aisle function
    private void LoadAisles()
    {
        var aisles = InventoryService.GetItems()
            .Select(item => item.Aisle)
            .Distinct()
            .ToList();
        AisleListView.ItemsSource = aisles;
    }
//Button Function
    private async void OnAisleTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is string aisle)
        {
            await Navigation.PushAsync(new ItemPage(aisle));
        }
    }
}
