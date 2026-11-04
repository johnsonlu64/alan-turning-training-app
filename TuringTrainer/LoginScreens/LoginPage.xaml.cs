using TuringTrainer.BusinessLogic;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer;
/*
 * Name : Bakary Ceesay
 * This is the code-behind file for the LoginPage.xaml page.
 * It contains the logic for handling user interactions on the LoginPage page.
 */

public partial class LoginPage : ContentPage
{
    //int count = 0;
	private readonly IUserService _userService;
    public LoginPage()
    {
		_userService = new UserService(new UserRepository());

        InitializeComponent();
        
    }

    private async void LoginButton_Clicked(object? sender, EventArgs e)
    {
		try
		{
			string email = EmailEntry.Text?.Trim() ?? "";
			string password = PasswordEntry.Text ?? "";

			UserSignInError result = await _userService.SignInUser(email, password);

			if (result == UserSignInError.NoError)
			{
				EmailEntry.Text = "";
				PasswordEntry.Text = "";
				await Shell.Current.GoToAsync(nameof(Homepage));
			}
			else
			{
				await DisplayAlertAsync("Signup failed", result.ToString(), "Ok");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlertAsync("Signup error", ex.Message, "Ok");
		}
	}

}

 
