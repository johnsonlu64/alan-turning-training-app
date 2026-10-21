
namespace TuringTrainer;

public partial class Homepage : ContentPage
{
    public Homepage()
    {
        InitializeComponent();
    }

    private void OnClass1Tapped(object sender, TappedEventArgs e)
    {
        // Open Class 1
    }

    private void OnClass2Tapped(object sender, TappedEventArgs e)
    {
        // Open Class 2
    }

    private void OnClass3Tapped(object sender, TappedEventArgs e)
    {
        // Open Class 3
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

    private void OnReportsClicked(object sender, EventArgs e)
    {
        // Open Reports page
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

    private void OnMetricsClicked(object sender, EventArgs e)
    {
        // Navigate to Metrics page
    }

    private void OnSettingsClicked(object sender, EventArgs e)
    {
        // Navigate to Settings page
    }

}
