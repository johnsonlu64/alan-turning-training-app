using TuringTrainer.BusinessLogic;
using TuringTrainer.Services.Repositories;
namespace TuringTrainer;
// dasha
public partial class ClassIPage : ContentPage
{
     private readonly IUserService _userService;
     private readonly UserRepository _userRepository;
    public ClassIPage()
    {        
        InitializeComponent();

        _userRepository = new UserRepository();
        _userService = new UserService(new UserRepository());

        SportsList.ItemsSource = new List<string>
        {
            "Powerlifting",
            "Wrestling"
        };

        RoutinesList.ItemsSource = Pages.Routine.All;

        MembersList.ItemsSource = _userRepository.Users;
    }

    private async void OnSportClicked(object? sender, EventArgs e)
    {
        // Open the sport
    }

    private async void OnMemberClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MetricsPage));
    }

    private void OnSportsTabClicked(object? sender, EventArgs e)
    {
        ShowSports();
    }

    private void OnRoutinesTabClicked(object? sender, EventArgs e)
    {
        ShowRoutines();
    }

    private async void OnMembersTabClicked(object? sender, EventArgs e)
    {
        await _userRepository.SelectAllUsers();
        ShowMembers();
    }


    private void ShowSports()
    {
        SportsTab.BackgroundColor = Color.FromArgb("#332B12");
        SportsTab.TextColor = Color.FromArgb("#F5C542");
        RoutinesTab.BackgroundColor = Colors.Transparent;
        RoutinesTab.TextColor = Color.FromArgb("#999999");
        MembersTab.BackgroundColor = Colors.Transparent;
        MembersTab.TextColor = Color.FromArgb("#999999");

        SportsList.IsVisible = true;
        RoutinesList.IsVisible = false;
        MembersList.IsVisible = false;
    }

    private void ShowRoutines()
    {
        SportsTab.BackgroundColor = Colors.Transparent;
        SportsTab.TextColor = Color.FromArgb("#999999");
        RoutinesTab.BackgroundColor = Color.FromArgb("#332B12");
        RoutinesTab.TextColor = Color.FromArgb("#F5C542");
        MembersTab.BackgroundColor = Colors.Transparent;
        MembersTab.TextColor = Color.FromArgb("#999999");

        SportsList.IsVisible = false;
        RoutinesList.IsVisible = true;
        MembersList.IsVisible = false;
    }

    private void ShowMembers()
    { 
        SportsTab.BackgroundColor = Colors.Transparent;
        SportsTab.TextColor = Color.FromArgb("#999999");
        RoutinesTab.BackgroundColor = Colors.Transparent;
        RoutinesTab.TextColor = Color.FromArgb("#999999");

        MembersTab.BackgroundColor = Color.FromArgb("#332B12");
        MembersTab.TextColor = Color.FromArgb("#F5C542");

        MembersList.IsVisible = true;
        SportsList.IsVisible = false;
        RoutinesList.IsVisible = false;
    }

    private async void OnRoutineSelected(object? sender, SelectionChangedEventArgs e)
    {
        Pages.Routine routine = (Pages.Routine)e.CurrentSelection[0];

        await Shell.Current.GoToAsync($"{nameof(Pages.RoutineBuilderPage)}?routineId={routine.Id}");
    }
}
