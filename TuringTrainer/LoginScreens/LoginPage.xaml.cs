namespace TuringTrainer;
//Name: Bakary
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

 
