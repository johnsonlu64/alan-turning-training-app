using TuringTrainer.Services;

namespace TuringTrainer;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		ThemeService.Initialize(this);
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}
