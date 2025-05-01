namespace BinaryBanditsGrocerySim;

public partial class SettingsPage : ContentPage
{
    public bool IsDarkMode { get; set; }

    public SettingsPage()
    {
        InitializeComponent();
        BindingContext = this;
        IsDarkMode = Preferences.Get("DarkMode", false);
        DarkModeSwitch.IsToggled = IsDarkMode;
    }

    private void OnDarkModeToggled(object sender, ToggledEventArgs e)
    {
        IsDarkMode = e.Value;
        Preferences.Set("DarkMode", IsDarkMode);
        if (Application.Current != null)
        {
            Application.Current.UserAppTheme = IsDarkMode ? AppTheme.Dark : AppTheme.Light;
        }
    }

    private async void OnBackToHomeClicked(object sender, EventArgs e) => await Navigation.PopAsync();
}