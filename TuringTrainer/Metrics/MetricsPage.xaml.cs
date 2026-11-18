using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;
using TuringTrainer.BusinessLogic;

namespace TuringTrainer;
// dasha
[QueryProperty(nameof(MemberId), "userId")]
public partial class MetricsPage : ContentPage
{
    private string _memberName = "";
    private string _memberId = "";
    private readonly IPersonalRecordService _personalRecordService;

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

            await _personalRecordService.SelectPersonalRecords(userId);
            DisplayPersonalRecords();

            if (!string.IsNullOrWhiteSpace(member.AvatarUrl))
            {
                AthleteAvatar.Source = ImageSource.FromUri(new Uri(member.AvatarUrl));
            }
            else
            {
                AthleteAvatar.Source = "blank_profile_picture.png";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", ex.Message, "OK");
        }
    }

    private void DisplayPersonalRecords()
    {
        Guid cleanId = Guid.Parse("576d09f7-9686-45a2-b3ba-e8753219e0b3");
        Guid backSquatId = Guid.Parse("45db5851-46a2-4711-a0c0-5d307cd9967d");
        Guid benchPressId = Guid.Parse("d9036c3b-b69d-4945-b6d9-13f2b0e97edc");

        var records = _personalRecordService.PersonalRecords;

        PersonalRecord? clean = null;
        PersonalRecord? backSquat = null;
        PersonalRecord? benchPress = null;

        foreach (PersonalRecord record in records)
        {
            if (record.ExerciseId == cleanId)
            {
                clean = record;
            }
            else if (record.ExerciseId == backSquatId)
            {
                backSquat = record;
            }
            else if (record.ExerciseId == benchPressId)
            {
                benchPress = record;
            }
        }

        if (clean != null && clean.Weight != null)
        {
            CleanLabel.Text = $"{clean.Weight} lbs";
        }
        else
        {
            CleanLabel.Text = "N/A";
        }

        if (backSquat != null && backSquat.Weight != null)
        {
            BackSquatLabel.Text = $"{backSquat.Weight} lbs";
        }
        else
        {
            BackSquatLabel.Text = "N/A";
        }

        if (benchPress != null && benchPress.Weight != null)
        {
            BenchPressLabel.Text = $"{benchPress.Weight} lbs";
        }
        else
        {
            BenchPressLabel.Text = "N/A";
        }
    }
    public MetricsPage()
    {
        InitializeComponent();
        _personalRecordService = new PersonalRecordService(new PersonalRecordRepository());
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
