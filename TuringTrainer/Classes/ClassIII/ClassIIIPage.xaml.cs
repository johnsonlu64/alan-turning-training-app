namespace TuringTrainer;
// dasha
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

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
    private async void OnSportClicked(object sender, EventArgs e)
    {
        // link to sport
    }
}