using TuringTrainer.BusinessLogic;
using TuringTrainer.Services.Repositories;
namespace TuringTrainer;
// dasha
public partial class ClassIIPage : ContentPage
{
    private readonly IUserService _userService;
    private readonly UserRepository _userRepository;
    public ClassIIPage()
    {
        _userRepository = new UserRepository();
        _userService = new UserService(new UserRepository());

        InitializeComponent();
        ShowSports();

        SportsList.ItemsSource = new List<string>
        {
            "Football",
            "Soccer",
            "Volleyball",
            "Basketball"
        };

        RoutinesList.ItemsSource = Pages.Routine.All;

        MembersList.ItemsSource = _userRepository.Users;
    }



    private async void OnSportClicked(object sender, EventArgs e)
    {

    }
    private async void OnMemberClicked(object sender, EventArgs e)
    {
        var clickedElement = sender as VisualElement;
        if (clickedElement == null) return;

        var member = clickedElement.BindingContext as Models.User;
        if (member == null) return;

        string memberName = $"{member.FirstName} {member.LastName}";

        await Shell.Current.GoToAsync($"{nameof(MetricsPage)}?name={Uri.EscapeDataString(memberName)}");
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
        SportsTab.SetDynamicResource(VisualElement.BackgroundColorProperty, "AccentTint");
        SportsTab.SetDynamicResource(Button.TextColorProperty, "TextAccent");
        RoutinesTab.BackgroundColor = Colors.Transparent;
        RoutinesTab.SetDynamicResource(Button.TextColorProperty, "TextMuted");
        MembersTab.BackgroundColor = Colors.Transparent;
        MembersTab.SetDynamicResource(Button.TextColorProperty, "TextMuted");

        SportsList.IsVisible = true;
        RoutinesList.IsVisible = false;
        MembersList.IsVisible = false;
    }

    private void ShowRoutines()
    {
        SportsTab.BackgroundColor = Colors.Transparent;
        SportsTab.SetDynamicResource(Button.TextColorProperty, "TextMuted");
        RoutinesTab.SetDynamicResource(VisualElement.BackgroundColorProperty, "AccentTint");
        RoutinesTab.SetDynamicResource(Button.TextColorProperty, "TextAccent");
        MembersTab.BackgroundColor = Colors.Transparent;
        MembersTab.SetDynamicResource(Button.TextColorProperty, "TextMuted");

        SportsList.IsVisible = false;
        RoutinesList.IsVisible = true;
        MembersList.IsVisible = false;
    }

    private void ShowMembers()
    { 
        SportsTab.BackgroundColor = Colors.Transparent;
        SportsTab.SetDynamicResource(Button.TextColorProperty, "TextMuted");
        RoutinesTab.BackgroundColor = Colors.Transparent;
        RoutinesTab.SetDynamicResource(Button.TextColorProperty, "TextMuted");

        MembersTab.SetDynamicResource(VisualElement.BackgroundColorProperty, "AccentTint");
        MembersTab.SetDynamicResource(Button.TextColorProperty, "TextAccent");

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
