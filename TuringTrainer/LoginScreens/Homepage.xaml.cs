
namespace TuringTrainer;

public partial class Homepage : ContentPage
{
    public Homepage()
    {
        InitializeComponent();
    }

    private async void OnClass1Tapped(object sender, TappedEventArgs e)
    {
        // Open Class 1
        await Shell.Current.GoToAsync(nameof(ClassIPage));
    }

    private async void OnClass2Tapped(object sender, TappedEventArgs e)
    {
        // Open Class 2
        await Shell.Current.GoToAsync(nameof(ClassIIPage));
    }

    private async void OnClass3Tapped(object sender, TappedEventArgs e)
    {
        // Open Class 3
        await Shell.Current.GoToAsync(nameof(ClassIIIPage));
    }

    private async void OnAddClassClicked(object sender, EventArgs e)
    {
        // Open Add Class flow
        await Shell.Current.GoToAsync(nameof(AddClass));
    }

    private void OnAddStudentClicked(object sender, EventArgs e)
    {
        // Open Add Student flow
    }

    private async void OnReportsClicked(object sender, EventArgs e)
    {
        // Open Reports page
        await Shell.Current.GoToAsync(nameof(Reports));
    }

    private void OnHomeClicked(object sender, EventArgs e)
    {
        // Navigate to Home page
    }

    private async void OnLeaderboardClicked(object sender, EventArgs e)
    {
        // Navigate to Leaderboard page
        await Shell.Current.GoToAsync(nameof(LeaderBoardPage));
    }

    private async void OnMetricsClicked(object sender, EventArgs e)
    {
        // Navigate to Metrics page
        await Shell.Current.GoToAsync(nameof(MetricsPage));
    }

    private void OnSettingsClicked(object sender, EventArgs e)
    {
        // Navigate to Settings page
    }

}
