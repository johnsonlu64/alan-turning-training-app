namespace TuringTrainer;

/*
 * Name : Bakary Ceesay
 * This is the code-behind file for the WelcomePage.xaml page.
 * It contains the logic for handling user interactions on the WelcomePage page.
 */

public partial class WelcomePage : ContentPage
{
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
