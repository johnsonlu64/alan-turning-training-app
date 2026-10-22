namespace TuringTrainer.Pages;
//Name: Luke Johnson

/// <summary>
/// The routine list. Tapping a routine opens the builder for it.
/// </summary>
public partial class RoutinesPage : ContentPage
{
    public RoutinesPage()
    {
        InitializeComponent();

        RoutinesList.ItemsSource = Routine.All;
    }

    private async void OnRoutineSelected(object? sender, SelectionChangedEventArgs e)
    {
        Routine routine = (Routine)e.CurrentSelection[0];

        await Shell.Current.GoToAsync($"{nameof(RoutineBuilderPage)}?routineId={routine.Id}");
    }

    private void OnJoinGroupClicked(object? sender, EventArgs e)
    {
        // Open Join Group flow
    }
}
