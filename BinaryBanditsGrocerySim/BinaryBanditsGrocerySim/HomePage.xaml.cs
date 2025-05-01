namespace BinaryBanditsGrocerySim;

using BinaryBanditsGrocerySim.Models;

public partial class HomePage : ContentPage
{
    private List<Person> Shoppers { get; set; }

    public HomePage()
    {
        Shoppers = new List<Person>();
        InitializeComponent();
        LoadShoppers();
    }

    private void LoadShoppers()
    {
        Shoppers = new List<Person>
        {
            new Person { Name = "Omar", CashBalance = 100.00 },
            new Person { Name = "Ryan", CashBalance = 100.00 },
            new Person { Name = "Kristian", CashBalance = 100.00 },
            
        };
        ShopperPicker.ItemsSource = Shoppers;
        var currentShopper = ShopperService.GetCurrentShopper();
        if (currentShopper != null)
        {
            ShopperPicker.SelectedItem = Shoppers.FirstOrDefault(s => s.Name == currentShopper.Name);
        }
    }

    private void OnShopperSelected(object sender, EventArgs e)
    {
        if (ShopperPicker.SelectedItem is Person selectedShopper)
        {
            ShopperService.SetCurrentShopper(selectedShopper);
        }
    }

    private async void OnBrowseAislesClicked(object sender, EventArgs e) => await Navigation.PushAsync(new AislePage());
    private async void OnViewCartClicked(object sender, EventArgs e) => await Navigation.PushAsync(new CartPage());
    private async void OnManageInventoryClicked(object sender, EventArgs e) => await Navigation.PushAsync(new InventoryManagerPage());
    private async void OnPromotionsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new PromotionsPage());
    private async void OnSettingsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new SettingsPage());
    private async void OnCustomerServiceClicked(object sender, EventArgs e) => await Navigation.PushAsync(new CustomerServicePage());
}