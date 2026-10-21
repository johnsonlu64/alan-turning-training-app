namespace TuringTrainer;

public partial class ClassIIIPage : ContentPage
{
    public ClassIIIPage()
    {
        InitializeComponent();

        SportsList.ItemsSource = new List<string>
        {
            "Dance",
            "Cross Country"
        };
    }

    private async void OnSportClicked(object sender, EventArgs e)
    {
        // link to sport
    }
}