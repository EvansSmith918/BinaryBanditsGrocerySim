// "
namespace BinaryBanditsGrocerySim;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		bool isDarkMode = Preferences.Get("DarkMode", false);
		UserAppTheme = isDarkMode ? AppTheme.Dark : AppTheme.Light;
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}
