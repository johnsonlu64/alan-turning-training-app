using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using TuringTrainer.LeaderBoard;

namespace TuringTrainer.Settings;

/// <summary>
/// Settings
/// Author: Aaditya Bhanadari
/// </summary>
public partial class Settings : ContentPage
{
	/// <summary>
	/// Themes
	/// </summary>
    private readonly string[] themes = { "System", "Light", "Dark" };

	/// <summary>
	/// Constructor
	/// </summary>	
    public Settings()
    {
        InitializeComponent();

        ThemePicker.ItemsSource = themes;
        ThemePicker.SelectedIndex = GetCurrentThemeIndex();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        NameEntry.Text ??= "Coach Name";
        EmailEntry.Text ??= "coach@uwosh.edu";
    }

    private int GetCurrentThemeIndex()
    {
        return Application.Current?.UserAppTheme switch
        {
            AppTheme.Light => 1,
            AppTheme.Dark => 2,
            _ => 0 // System
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
        await Shell.Current.GoToAsync("HelpPage");
    }

    private async void OnFeedbackTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("FeedbackPage");
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

        Preferences.Remove("auth_token");

        // LoginPage is registered in AppShell.
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
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