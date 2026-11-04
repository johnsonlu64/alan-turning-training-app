using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer;
/*
 * Name : Bakary Ceesay
 * This is the code-behind file for the SignupPage.xaml page.
 * It contains the logic for handling user interactions on the SignupPage page.
 */

public partial class SignupPage : ContentPage
{
	private readonly UserService _userService;
	
	public SignupPage()
	{
		_userService = new UserService(new UserRepository());

		InitializeComponent();

		
	}


	public async void SignupButton_Clicked(object sender, EventArgs e)
	{
		try
		{
			string email = EmailEntry.Text?.Trim() ?? "";
			string password = PasswordEntry.Text ?? "";

			UserAdditionError result = await _userService.SignUpUser(email, password);

			if (result == UserAdditionError.NoError)
			{
				EmailEntry.Text = "";
				PasswordEntry.Text = "";
				await Shell.Current.GoToAsync(nameof(Homepage));
			}
			else
			{
				await DisplayAlert("Signup failed", result.ToString(), "Ok");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Signup error", ex.Message, "Ok");
		}
	}
}
