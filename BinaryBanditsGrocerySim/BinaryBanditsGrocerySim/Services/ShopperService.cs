namespace BinaryBanditsGrocerySim;

using BinaryBanditsGrocerySim.Models;

public static class ShopperService
{
    private static Person? _currentShopper;

    public static Person GetCurrentShopper()
    {
        if (_currentShopper == null)
        {
            string selectedShopperName = Preferences.Get("SelectedShopper", string.Empty);
            if (!string.IsNullOrEmpty(selectedShopperName))
            {
                var shoppers = new List<Person>
                {
                    new Person { Name = "John Doe", CashBalance = 100.00 },
                    new Person { Name = "Jane Smith", CashBalance = 75.50 },
                    new Person { Name = "Alice Johnson", CashBalance = 50.25 }
                };
                _currentShopper = shoppers.FirstOrDefault(s => s.Name == selectedShopperName) ?? new Person { Name = selectedShopperName, CashBalance = 0.0 };
                // Load the saved balance if it exists
                if (_currentShopper != null)
                {
                    double savedBalance = Preferences.Get($"Balance_{_currentShopper.Name}", _currentShopper.CashBalance);
                    _currentShopper.CashBalance = savedBalance;
                }
            }
        }
        return _currentShopper ??= new Person { Name = "Default Shopper", CashBalance = 0.0 };
    }

    public static void SetCurrentShopper(Person shopper)
    {
        _currentShopper = shopper;
        if (shopper != null)
        {
            Preferences.Set("SelectedShopper", shopper.Name);
            // Save the balance
            Preferences.Set($"Balance_{shopper.Name}", shopper.CashBalance);
        }
        else
        {
            Preferences.Set("SelectedShopper", string.Empty);
        }
    }

    public static void UpdateBalance(double amountSpent)
    {
        if (_currentShopper != null)
        {
            _currentShopper.CashBalance -= amountSpent;
            Preferences.Set($"Balance_{_currentShopper.Name}", _currentShopper.CashBalance);
        }
    }
}