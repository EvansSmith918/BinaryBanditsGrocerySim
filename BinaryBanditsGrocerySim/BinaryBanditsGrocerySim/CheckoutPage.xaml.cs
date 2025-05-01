namespace BinaryBanditsGrocerySim;

using BinaryBanditsGrocerySim.Models;

public partial class CheckoutPage : ContentPage
{
    private double _totalPrice;
    private double _discountAmount;

    public CheckoutPage()
    {
        InitializeComponent();
        LoadCheckout();
    }

    private void LoadCheckout()
    {
        var cartItems = CartService.GetCartItems();
        _totalPrice = cartItems.Sum(item => item.TotalCost);
        TotalPriceLabel.Text = $"Total: {_totalPrice:C}";
    }

    private async void OnApplyDiscountClicked(object sender, EventArgs e)
    {
        string code = (DiscountCodeEntry.Text?.Trim().ToUpper()) ?? string.Empty;
        if (string.IsNullOrEmpty(code))
        {
            await DisplayAlert("Error", "Please enter a discount code.", "OK");
            return;
        }

        _discountAmount = 0;
        switch (code)
        {
            case "BOGO":
                var firstItem = CartService.GetCartItems().FirstOrDefault();
                if (firstItem != null && firstItem.Quantity >= 2)
                {
                    _discountAmount = firstItem.Price * (firstItem.Quantity / 2);
                }
                break;
            case "10OFF":
                _discountAmount = _totalPrice * 0.10;
                break;
            case "2DOLLARSOFF":
                _discountAmount = 2.00;
                break;
            default:
                await DisplayAlert("Error", "Invalid discount code.", "OK");
                return;
        }

        DiscountLabel.Text = $"Discount Applied: {_discountAmount:C}";
        TotalPriceLabel.Text = $"Total: {(_totalPrice - _discountAmount):C}";
    }

    private async void OnConfirmPaymentClicked(object sender, EventArgs e)
    {
        var shopper = ShopperService.GetCurrentShopper();

        if (shopper == null || (_totalPrice - _discountAmount) > shopper.CashBalance)
        {
            await DisplayAlert("Error", "Insufficient funds.", "OK");
            return;
        }

        if (string.IsNullOrEmpty(CardNumberEntry.Text) || string.IsNullOrEmpty(ExpirationDateEntry.Text) ||
            string.IsNullOrEmpty(CVVEntry.Text) || string.IsNullOrEmpty(CardHolderNameEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all payment details.", "OK");
            return;
        }

        if (RewardsOptInCheckBox.IsChecked && !string.IsNullOrEmpty(EmailEntry.Text))
        {
            Preferences.Set("RewardsEmail", EmailEntry.Text);
        }

        // Update the shopper's balance
        ShopperService.UpdateBalance(_totalPrice - _discountAmount);

        InventoryService.RemoveItems(CartService.GetCartItems());
        CartService.ClearCart();
        await DisplayAlert("Success", "Payment completed! Thank you for shopping.", "OK");
        // Pop to root and select the Home tab
        await Shell.Current.Navigation.PopToRootAsync();
        Shell.Current.CurrentItem = Shell.Current.Items.FirstOrDefault(item => item.Route == "HomePage");
    }
}