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
		Routing.RegisterRoute(nameof(ClassIPage), typeof(ClassIPage));
		Routing.RegisterRoute(nameof(ClassIIPage), typeof(ClassIIPage));
		Routing.RegisterRoute(nameof(ClassIIIPage), typeof(ClassIIIPage));
		Routing.RegisterRoute(nameof(Pages.RoutinesPage), typeof(Pages.RoutinesPage));
		Routing.RegisterRoute(nameof(Pages.RoutineBuilderPage), typeof(Pages.RoutineBuilderPage));
		Routing.RegisterRoute(nameof(Pages.NotificationsPage), typeof(Pages.NotificationsPage));
	}
}
