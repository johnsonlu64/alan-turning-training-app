using TuringTrainer.BusinessLogic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using TuringTrainer.Services.Repositories;
using TuringTrainer.Models;

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

    private readonly IUserService _userService;

    /// <summary>
    /// Constructor
    /// </summary>	
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

        NameEntry.Text ??= "Coach Name";
        EmailEntry.Text ??= "coach@uwosh.edu";

        User? currentUser = await _userService.CurrentUser();

        if (!string.IsNullOrWhiteSpace(currentUser?.AvatarUrl))
        {
            ProfileImage.Source = ImageSource.FromUri(new Uri(currentUser.AvatarUrl));
        }
        else
        {
            ProfileImage.Source = "blank_profile_picture.png";
        }
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

    private async void OnProfilePictureTapped(object sender, TappedEventArgs e)
    {
        string action = await DisplayActionSheetAsync(
            "Change Photo",
            "Cancel",
            null,
            "Upload Photo",
            "Remove Photo");

        if (action == "Upload Photo")
        {
            await ChangeProfilePicture();
        }
        else if (action == "Remove Photo")
        {
            await RemoveProfilePicture();
        }
    }

    private async Task ChangeProfilePicture()
    {
        try
        {
            var photo = await MediaPicker.Default.PickPhotoAsync();
            if (photo == null)
            {
                return;
            }

            string? userId = _userService.CurrentUserId();

            User? currentUser = await _userService.CurrentUser();
            if (currentUser == null)
            {
                await DisplayAlertAsync("Error", "User not found.", "OK");
                return;
            }

            UserAvatarError result = await _userService.UploadAvatar(currentUser.UserId, photo);
            if (result == UserAvatarError.NoError)
            {
                User? updatedUser = await _userService.CurrentUser();

                if (!string.IsNullOrWhiteSpace(updatedUser?.AvatarUrl))
                {
                    ProfileImage.Source = ImageSource.FromUri(new Uri(updatedUser.AvatarUrl));
                }
            }
            else
            {
                await DisplayAlertAsync("Upload Failed", result.ToString(), "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", ex.Message, "OK");
        }
    }

    private async Task RemoveProfilePicture()
    {
        bool confirm = await DisplayAlertAsync(
            "Remove Photo",
            "Are you sure you want to remove your profile picture?",
            "Remove",
            "Cancel");

        if (!confirm) { return; }

        try
        {
            User? currentUser = await _userService.CurrentUser();

            if (currentUser == null)
            {
                await DisplayAlertAsync("Error", "User not found.", "OK");
                return;
            }

            UserAvatarError result = await _userService.UpdateAvatar(currentUser.UserId, null);

            if (result == UserAvatarError.NoError)
            {
                ProfileImage.Source = "blank_profile_picture.png";
            }
            else
            {
                await DisplayAlertAsync("Remove Failed", result.ToString(), "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", ex.Message, "OK");
        }
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

        // LoginPage is registered in AppShell.
        if (result == UserSignoutError.NoError)
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
        else
        {
            await DisplayAlertAsync("ErrorSigningOut", UserSignoutError.DBSignoutError.ToString(), "Ok");
        }
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