namespace BinaryBanditsGrocerySim;

public partial class CustomerServicePage : ContentPage
{
    public CustomerServicePage()
    {
        InitializeComponent();
    }

    private async void OnBackToHomeClicked(object sender, EventArgs e) => await Navigation.PopAsync();
}