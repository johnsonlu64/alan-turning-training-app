using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using TuringTrainer.BusinessLogic;
using TuringTrainer.Models;
using TuringTrainer.Services;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.Settings;

public partial class Settings : ContentPage
{
    private User? _currentUser;

    private readonly string[] themes =
    {
        "System",
        "Light",
        "Dark"
    };

    private readonly IUserService _userService;

    private readonly IOrganizationService _organizationService =
        new OrganizationService();

    public Settings()
    {
        _userService = new UserService(new UserRepository());

        InitializeComponent();

        ThemePicker.ItemsSource = themes;
        ThemePicker.SelectedIndex = (int)ThemeService.Current;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _currentUser = await _userService.CurrentUser();

        if (_currentUser != null)
        {
            ShowAccountInformation();
        }

        string? email = _userService.GetUserSession()?.User?.Email;
        EmailLabel.Text = email ?? "";

        await LoadOrganizationName();

        if (!string.IsNullOrWhiteSpace(_currentUser?.AvatarUrl))
        {
            ProfileImage.Source = ImageSource.FromUri(
                new Uri(_currentUser.AvatarUrl));
        }
        else
        {
            ProfileImage.Source = "blank_profile_picture.png";
        }
    }

    private async Task LoadOrganizationName()
    {
        try
        {
            Organization? organization =
                await _organizationService.GetCurrentOrganization();

            if (organization != null)
            {
                OrganizationNameLabel.Text = organization.Name;
                AccountOrganizationLabel.Text = organization.Name;
                OrganizationIdLabel.Text =
                    organization.OrganizationId.ToString();
            }
            else
            {
                OrganizationNameLabel.Text = "No organization";
                AccountOrganizationLabel.Text = "No organization";
                OrganizationIdLabel.Text = string.Empty;
            }
        }
        catch
        {
            OrganizationNameLabel.Text = "No organization";
            AccountOrganizationLabel.Text = "No organization";
            OrganizationIdLabel.Text = string.Empty;
        }
    }

    private void OnThemeChanged(object sender, EventArgs e)
    {
        if (ThemePicker.SelectedIndex < 0)
            return;

        ThemeChoice choice =
            (ThemeChoice)ThemePicker.SelectedIndex;

        ThemeService.Apply(choice);
    }

    private async void OnProfilePictureTapped(
        object sender,
        TappedEventArgs e)
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
                return;

            User? currentUser =
                await _userService.CurrentUser();

            if (currentUser == null)
            {
                await DisplayAlertAsync(
                    "Error",
                    "User not found.",
                    "OK");

                return;
            }

            UserAvatarError result =
                await _userService.UploadAvatar(
                    currentUser.UserId,
                    photo);

            if (result == UserAvatarError.NoError)
            {
                User? updatedUser =
                    await _userService.CurrentUser();

                if (!string.IsNullOrWhiteSpace(updatedUser?.AvatarUrl))
                {
                    _currentUser = updatedUser;

                    ProfileImage.Source = ImageSource.FromUri(
                        new Uri(updatedUser.AvatarUrl));
                }
            }
            else
            {
                await DisplayAlertAsync(
                    "Upload Failed",
                    result.ToString(),
                    "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Error",
                ex.Message,
                "OK");
        }
    }

    private async Task RemoveProfilePicture()
    {
        bool confirm = await DisplayAlertAsync(
            "Remove Photo",
            "Are you sure you want to remove your profile picture?",
            "Remove",
            "Cancel");

        if (!confirm)
            return;

        try
        {
            User? currentUser =
                await _userService.CurrentUser();

            if (currentUser == null)
            {
                await DisplayAlertAsync(
                    "Error",
                    "User not found.",
                    "OK");

                return;
            }

            UserAvatarError result =
                await _userService.UpdateAvatar(
                    currentUser.UserId,
                    null);

            if (result == UserAvatarError.NoError)
            {
                currentUser.AvatarUrl = null;
                _currentUser = currentUser;

                ProfileImage.Source =
                    "blank_profile_picture.png";
            }
            else
            {
                await DisplayAlertAsync(
                    "Remove Failed",
                    result.ToString(),
                    "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Error",
                ex.Message,
                "OK");
        }
    }

    private async void OnSaveClicked(
        object sender,
        EventArgs e)
    {
        if (_currentUser == null)
        {
            await DisplayAlertAsync(
                "Error",
                "Could not find your account information.",
                "OK");

            return;
        }

        string firstName =
            FirstNameEntry.Text?.Trim() ?? "";

        string middleInitial =
            MiddleInitialEntry.Text?.Trim() ?? "";

        string lastName =
            LastNameEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(firstName))
        {
            await DisplayAlertAsync(
                "Missing Information",
                "Please enter your first name.",
                "OK");

            return;
        }

        UserEditError result = await _userService.UpdateUser(
            _currentUser,
            firstName,
            middleInitial,
            lastName,
            _currentUser.IsCoach);

        if (result == UserEditError.NoError)
        {
            _currentUser.FirstName = firstName;
            _currentUser.MiddleInitial = middleInitial;
            _currentUser.LastName = lastName;

            ShowAccountInformation();

            EditAccountPopup.IsVisible = false;

            await DisplayAlertAsync(
                "Success",
                "Your account information has been updated.",
                "OK");
        }
        else
        {
            await DisplayAlertAsync(
                "Update Failed",
                $"Update result: {result}",
                "OK");
        }
    }

    private async void OnHelpTapped(
        object sender,
        TappedEventArgs e)
    {
        await DisplayAlertAsync(
            "Coming Soon!",
            "The Help feature will be available soon.",
            "OK");
    }

    private async void OnFeedbackTapped(
        object sender,
        TappedEventArgs e)
    {
        await DisplayAlertAsync(
            "Coming Soon!",
            "The Feedback feature will be available soon.",
            "OK");
    }

    private async void OnLogoutClicked(
        object sender,
        EventArgs e)
    {
        bool confirmed = await DisplayAlertAsync(
            "Log Out",
            "Are you sure you want to log out?",
            "Log Out",
            "Cancel");

        if (!confirmed)
            return;

        string? currentUserId =
            _userService.CurrentUserId();

        UserSignoutError result =
            await _userService.SignOutUser();

        if (result == UserSignoutError.NoError)
        {
            if (Guid.TryParse(currentUserId, out Guid userId))
            {
                await _organizationService.ClearCurrentOrganization(
                    userId);
            }

            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
        else
        {
            await DisplayAlertAsync(
                "ErrorSigningOut",
                UserSignoutError.DBSignoutError.ToString(),
                "OK");
        }
    }

    private async void OnHomeClicked(
        object sender,
        EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Homepage));
    }

    private async void OnLeaderboardClicked(
        object sender,
        EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LeaderBoardPage));
    }

    private async void OnMetricsClicked(
        object sender,
        EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ClassesPage));
    }

    private void ShowAccountInformation()
    {
        if (_currentUser == null)
            return;

        FirstNameLabel.Text = _currentUser.FirstName;
        MiddleInitialLabel.Text = _currentUser.MiddleInitial;
        LastNameLabel.Text = _currentUser.LastName;
    }

    private void OnEditClicked(
        object sender,
        EventArgs e)
    {
        if (_currentUser == null)
            return;

        FirstNameEntry.Text = _currentUser.FirstName;
        MiddleInitialEntry.Text = _currentUser.MiddleInitial;
        LastNameEntry.Text = _currentUser.LastName;

        EditAccountPopup.IsVisible = true;
    }

    private void OnCancelClicked(
        object sender,
        EventArgs e)
    {
        EditAccountPopup.IsVisible = false;
    }
}