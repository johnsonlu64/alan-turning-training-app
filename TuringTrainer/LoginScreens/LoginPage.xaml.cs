namespace TuringTrainer;
/*
 * Name : Bakary Ceesay
 * This is the code-behind file for the LoginPage.xaml page.
 * It contains the logic for handling user interactions on the LoginPage page.
 */

public partial class LoginPage : ContentPage
{
    //int count = 0;

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void LoginButton_Clicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Homepage));
    }

}

 
