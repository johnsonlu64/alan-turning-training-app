using TuringTrainer.Services.Repositories;
namespace TuringTrainer;
// dasha
public partial class ClassesPage : ContentPage
{
    private static readonly Guid CoachId = Guid.Parse("4daa0811-e8c6-431a-b4f8-21b05ce2d78f");
    private readonly IClassRepository _classRepository = new ClassRepository();
    public ClassesPage()
    {
        InitializeComponent();
        BindableLayout.SetItemsSource(ClassListLayout, _classRepository.Classes);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            await _classRepository.SelectClassesByCoachAsync(CoachId);

            int totalClassCount = _classRepository.Classes.Count;

            ClassCountLabel.Text = $"{totalClassCount} {(totalClassCount == 1 ? "class" : "classes")}";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load classes: {ex.Message}", "OK");
        }
    }

    private async void OnClassTapped(object sender, TappedEventArgs e)
    {
        var selected = (sender as Element)?.BindingContext as Models.Class;
        if (selected == null) return;

        await Shell.Current.GoToAsync(
            $"{nameof(ClassPage)}?classId={selected.ClassId}&className={Uri.EscapeDataString(selected.Name ?? string.Empty)}");
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