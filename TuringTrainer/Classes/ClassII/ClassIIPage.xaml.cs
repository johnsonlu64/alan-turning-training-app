namespace TuringTrainer;

public partial class ClassIIPage : ContentPage
{
    public ClassIIPage()
    {
        InitializeComponent();

        SportsList.ItemsSource = new List<string>
        {
            "Football",
            "Soccer",
            "Volleyball",
            "Basketball"
        };
    }

        private async void OnSportClicked(object sender, EventArgs e)
    {
        
    }


}