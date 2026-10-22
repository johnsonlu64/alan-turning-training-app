namespace TuringTrainer;

public partial class ClassIPage : ContentPage
{
    public ClassIPage()
    {
        InitializeComponent();

        SportsList.ItemsSource = new List<string>
        {
            "Powerlifting",
            "Wrestling"
        };

        RoutinesList.ItemsSource = Pages.Routine.All;
    }

    private async void OnSportClicked(object? sender, EventArgs e)
    {
        // Open the sport
    }

    private void OnSportsTabClicked(object? sender, EventArgs e)
    {
        ShowSports();
    }

    private void OnRoutinesTabClicked(object? sender, EventArgs e)
    {
        ShowRoutines();
    }

    private void ShowSports()
    {
        SportsTab.BackgroundColor = Color.FromArgb("#332B12");
        SportsTab.TextColor = Color.FromArgb("#F5C542");
        RoutinesTab.BackgroundColor = Colors.Transparent;
        RoutinesTab.TextColor = Color.FromArgb("#999999");

        SportsList.IsVisible = true;
        RoutinesList.IsVisible = false;
    }

    private void ShowRoutines()
    {
        SportsTab.BackgroundColor = Colors.Transparent;
        SportsTab.TextColor = Color.FromArgb("#999999");
        RoutinesTab.BackgroundColor = Color.FromArgb("#332B12");
        RoutinesTab.TextColor = Color.FromArgb("#F5C542");

        SportsList.IsVisible = false;
        RoutinesList.IsVisible = true;
    }

    private async void OnRoutineSelected(object? sender, SelectionChangedEventArgs e)
    {
        Pages.Routine routine = (Pages.Routine)e.CurrentSelection[0];

        await Shell.Current.GoToAsync($"{nameof(Pages.RoutineBuilderPage)}?routineId={routine.Id}");
    }
}
