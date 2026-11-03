using TuringTrainer.LeaderBoard;

namespace TuringTrainer;
/// <summary>
/// LeaderBoardPage
/// Author: Aaditya Bhanadari
/// </summary>
public partial class LeaderBoardPage : ContentPage
{
	public LeaderBoardPage()
	{
		InitializeComponent();

		ClassPicker.Items.Add("All Classes");
        foreach (var schoolClass in classes)
        {
            ClassPicker.Items.Add(schoolClass.Name);
		}

        ClassPicker.SelectedIndex = 0;
	}

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

	private void OnClassChanged(object sender, EventArgs e)
    {
        var athletes = ClassPicker.SelectedIndex <= 0
            ? classes.SelectMany(c => c.Athletes) // All Classes
            : classes[ClassPicker.SelectedIndex - 1].Athletes; // One class

        Board.ItemsSource = athletes.OrderByDescending(a => a.Points).ToList();
    }
 
}