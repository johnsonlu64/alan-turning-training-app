namespace TuringTrainer;

public partial class WelcomePage : ContentPage
{
	int count = 0;

	public WelcomePage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object? sender, EventArgs e)
	{
		return;
	}

	private async void LoginButton_Clicked(object? sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(LoginPage));
	}

	private async void SignupButton_Clicked(object? sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("SignupPage");
	}
}
