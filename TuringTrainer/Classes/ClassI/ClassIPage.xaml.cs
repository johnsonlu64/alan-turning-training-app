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
    }

        private async void OnSportClicked(object sender, EventArgs e)
    {
        
    }


}