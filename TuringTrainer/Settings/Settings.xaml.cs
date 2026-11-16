using TuringTrainer.BusinessLogic;
using TuringTrainer.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using TuringTrainer.Services;
using TuringTrainer.Services.Repositories;


namespace TuringTrainer.Settings;

/// <summary>
/// Settings
/// Author: Aaditya Bhanadari
/// </summary>
public partial class Settings : ContentPage
{
    private User? _currentUser;
    private readonly string[] themes = { "System", "Light", "Dark" };

    private readonly IUserService _userService;

    public Settings()
    {
        _userService = new UserService(new UserRepository());

        InitializeComponent();

        ThemePicker.ItemsSource = themes;
        ThemePicker.SelectedIndex = GetCurrentThemeIndex();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _currentUser = await _userService.CurrentUser();

        if (_currentUser != null)
        {
            FirstNameEntry.Text = _currentUser.FirstName;
            MiddleInitialEntry.Text = _currentUser.MiddleInitial;
            LastNameEntry.Text = _currentUser.LastName;
        }

        string? email = _userService.GetUserSession()?.User?.Email;

        EmailEntry.Text = email ?? "";
        
    }

    private int GetCurrentThemeIndex()
    {
        return Application.Current?.UserAppTheme switch
        {
            AppTheme.Light => 1,
            AppTheme.Dark => 2,
            _ => 0 
        };
    }

    private void OnThemeChanged(object sender, EventArgs e)
    {
        if (Application.Current is null || ThemePicker.SelectedIndex < 0)
            return;

        Application.Current.UserAppTheme = ThemePicker.SelectedIndex switch
        {
            1 => AppTheme.Light,
            2 => AppTheme.Dark,
            _ => AppTheme.Unspecified
        };
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync("Saved", "Your settings have been saved.", "OK");
    }

    private async void OnHelpTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlertAsync(
        "Coming Soon!",
        "The Help feature will be available soon.",
        "OK");
    }

    private async void OnFeedbackTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlertAsync(
        "Coming Soon!",
        "The Feedback feature will be available soon.",
        "OK");
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirmed = await DisplayAlertAsync(
            "Log Out",
            "Are you sure you want to log out?",
            "Log Out",
            "Cancel");

        if (!confirmed)
            return;

        UserSignoutError result = await _userService.SignOutUser();

        if (result == UserSignoutError.NoError)
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
        else
        {
            await DisplayAlertAsync("ErrorSigningOut", UserSignoutError.DBSignoutError.ToString(), "Ok");
        }
    }

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Homepage));
    }

    private async void OnLeaderboardClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LeaderBoardPage));
    }

    private async void OnMetricsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("MetricsPage");
    }
}