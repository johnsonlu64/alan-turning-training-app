namespace TuringTrainer;
// dasha
public partial class ClassesPage : ContentPage
{
    public ClassesPage()
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