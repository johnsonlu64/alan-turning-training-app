
using TuringTrainer.LeaderBoard;

namespace TuringTrainer;

public partial class Homepage : ContentPage
{
    /// <summary>
    /// Class 1 - Class 3 with athletes
    /// </summary>
    private readonly List<CLasses> classes = new()
    {
        new CLasses
        {
            Name = "Class 1",
            Athletes = new List<Athlete>
            {
                new Athlete { Name = "Alex Morgan", Points = 1250 },
                new Athlete { Name = "Sam Kerr", Points = 1100 },
                new Athlete { Name = "Jordan Lee", Points = 950 }
            }
        },
        new CLasses
        {
            Name = "Class 2",
            Athletes = new List<Athlete>
            {
                new Athlete { Name = "Taylor Smith", Points = 1400 },
                new Athlete { Name = "Chris Brown", Points = 1200 },
                new Athlete { Name = "Jamie Wilson", Points = 900 }
            }
        },
        new CLasses
        {
            Name = "Class 3",
            Athletes = new List<Athlete>
            {
                new Athlete { Name = "Morgan Davis", Points = 1350 },
                new Athlete { Name = "Riley Chen", Points = 1150 },
                new Athlete { Name = "Casey Jones", Points = 1000 }
            }
        }
    };

    public Homepage()
    {
        InitializeComponent();

        //fill the class with atheletes
        ClassPicker.Items.Add("Class 1");
        ClassPicker.Items.Add("Class 2");
        ClassPicker.Items.Add("Class 3");
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

    private void OnAddClassClicked(object sender, EventArgs e)
    {
        // Open Add Class flow
    }

    /// <summary>
    /// Opens the Add Student popup
    /// Author: Aaditya Bhanadari
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnAddStudentClicked(object sender, EventArgs e)
    {
        ClassPicker.SelectedIndex = 0;
        StudentIdEntry.Text = string.Empty;
        AddStudentPopup.IsVisible = true;
    }

    private void OnStudentIdEntryCompleted(object sender, EventArgs e)
    {
        string id = StudentIdEntry.Text;
        if (string.IsNullOrEmpty(id))
        {
            return;
        }
        classes[ClassPicker.SelectedIndex].Athletes.Add(new Athlete { Name = id, Points = 0 });

        AddStudentPopup.IsVisible = false;
    }

    private void CloseAddStudentPopup(object sender, EventArgs e)
    {
        AddStudentPopup.IsVisible = false;
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
