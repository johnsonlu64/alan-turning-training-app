using Microsoft.Extensions.DependencyInjection;

namespace TuringTrainer;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		// The app is designed dark, so the Shell navigation bar follows the dark palette.
		UserAppTheme = AppTheme.Dark;
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}
