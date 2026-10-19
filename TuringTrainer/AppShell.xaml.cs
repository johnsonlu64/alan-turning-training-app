namespace TuringTrainer;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute("SignupPage", typeof(SignupPage));
		Routing.RegisterRoute(nameof(LeaderBoardPage), typeof(LeaderBoardPage));
		Routing.RegisterRoute(nameof(Homepage), typeof(Homepage));
	}
}
