using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer;
// dasha
[QueryProperty(nameof(MemberId), "userId")]
public partial class MetricsPage : ContentPage
{
    private string _memberName = "";
    private string _memberId = "";

    public string MemberId
    {
        get { return _memberId; }
        set
        {
            _memberId = value;
            _ = LoadMember();
        }
    }

     public string MemberName
    {
        get { return _memberName; }
        set
        {
            _memberName = value;
            OnPropertyChanged();
        }
    }

    private async Task LoadMember()
    {
        if (!Guid.TryParse(MemberId, out Guid userId))
        {
            return;
        }
        try
        {
            UserRepository repository = new UserRepository();
            User? member = await repository.SelectUser(userId);

            if (member == null)
                return;

            MemberName = $"{member.FirstName} {member.LastName}";

            AthleteAvatar.Source =
                !string.IsNullOrWhiteSpace(member.AvatarUrl)
                    ? ImageSource.FromUri(new Uri(member.AvatarUrl))
                    : ImageSource.FromFile("blank_profile_picture.png");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", ex.Message, "OK");
        }
    }
    public MetricsPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    // When a item from the drop down is selected this method is triggered and
    // fills in the weights depending on which exercise was selected calls 
    // update chart method
    private void ExercisePicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        double[] weights;

        if (ExercisePicker.SelectedIndex == 0)
        {
            weights = new double[] { 145, 155, 160, 175, 185 };
        }
        else if (ExercisePicker.SelectedIndex == 1)
        {
            weights = new double[] { 225, 235, 245, 260, 275 };
        }
        else if (ExercisePicker.SelectedIndex == 2)
        {
            weights = new double[] { 265, 275, 290, 300, 315 };
        }
        else
        {
            return;  // if nothing is selected yet don't display anything currently
        }

        UpdateChart(weights);
        
    }

    // This method takes in the weight points for the chart and uses those to make a 
    // line then sets the xasis as custom month names. The y axis is automatically 
    // made based off the values in the LineSeries
    private void UpdateChart(double[] weights)
    {
        StrengthChart.Series = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = weights,
            }
        };
        
        StrengthChart.XAxes = new Axis[]
        {
            new Axis
            {
                Labels = new string[] {"Sept", "Oct", "Nov", "Dec", "Jan" }
            }
        };
    }

}
