using System.Collections.ObjectModel;
using TuringTrainer.BusinessLogic;
using TuringTrainer.Services.Repositories;
namespace TuringTrainer;
// dasha
public partial class ClassesPage : ContentPage
{
   private readonly IUserRepository _userRepository = new UserRepository();

    private readonly ObservableCollection<Models.User> _allMembers = new();
    public ObservableCollection<Models.User> FilteredMembers { get; } = new();

    public ClassesPage()
    {
        InitializeComponent();
        MembersList.ItemsSource = FilteredMembers;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            await _userRepository.SelectAllUsers();

            _allMembers.Clear();

            foreach (Models.User member in _userRepository.Users)
            {
                if (!member.IsCoach)
                {
                    _allMembers.Add(member);
                }
            }

            ApplyFilter();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", $"Could not load members: {ex.Message}", "OK");
        }
    }

    private async void OnMemberClicked(object sender, EventArgs e)
    {
        var clickedElement = sender as VisualElement;
        if (clickedElement == null) return;

        var member = clickedElement.BindingContext as Models.User;
        if (member == null) return;

        await Shell.Current.GoToAsync($"{nameof(MetricsPage)}?userId={member.UserId}");
    }

    private void OnMemberSearchTextChanged(object? sender, TextChangedEventArgs e) => ApplyFilter();

    private void ApplyFilter()
    {
        string search = MemberSearchBar.Text ?? "";
        FilteredMembers.Clear();
        foreach (Models.User member in _allMembers.OrderBy(m => m.LastName))
        {
            string fullName = member.FirstName + " " + member.LastName;
            if (fullName.Contains(search, StringComparison.OrdinalIgnoreCase))
                FilteredMembers.Add(member);
        }
    }

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        // Navigate to Home page
        await Shell.Current.GoToAsync(nameof(Homepage));
    }

    private async void OnLeaderboardClicked(object sender, EventArgs e)
    {
        // Navigate to Leaderboard page
        await Shell.Current.GoToAsync(nameof(LeaderBoardPage));
    }

    private async void OnMetricsClicked(object sender, EventArgs e)
    {
        // Navigate to Metrics page
        await Shell.Current.GoToAsync(nameof(ClassesPage));
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SettingsPage");
    }
}