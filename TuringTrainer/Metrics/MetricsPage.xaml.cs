namespace TuringTrainer;

public partial class MetricsPage : ContentPage
{
    public MetricsPage()
    {
        InitializeComponent();
    }

    private void HistoryButton_Clicked(object sender, EventArgs e)
    {
        HistoryLayout.IsVisible = !HistoryLayout.IsVisible;

        if (HistoryLayout.IsVisible)
        {
            HistoryButton.Text = "History  ^";

        } else
        {
            HistoryButton.Text = "History  v";
        }
    }

    private void PastWorkout_Clicked(object sender, EventArgs e)
    {
    }

    private void SeeAllButton_Clicked(object sender, EventArgs e)
    {
        MoreHistoryLayout.IsVisible = !MoreHistoryLayout.IsVisible;

        if (MoreHistoryLayout.IsVisible)
        {
            SeeAllButton.Text = "Show Less";
        }
        else
        {
            SeeAllButton.Text = "See All";
        }
    }
}
