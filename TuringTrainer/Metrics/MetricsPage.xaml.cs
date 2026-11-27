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
    private readonly IRoutineEntryService _routineEntryService;

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
            DisplayLatestPR();
            ExercisePicker.SelectedIndex = 0;
            ExercisePicker_SelectedIndexChanged(null, EventArgs.Empty);

            TotalPRsLabel.Text = _personalRecordService.PersonalRecords.Count.ToString();
            int workouts = await _routineEntryService.SelectCompletedWorkouts(userId);
            WorkoutsLabel.Text = workouts.ToString();

            DateTime? lastWorkout = await _routineEntryService.SelectLastWorkoutDate(userId);

            if (lastWorkout != null)
            {
                ImprovementLabel.Text = lastWorkout.Value.ToShortDateString();
            }
            else
            {
                ImprovementLabel.Text = "N/A";
            }

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

    private void DisplayLatestPR()
    {
        var records = _personalRecordService.PersonalRecords;

        PersonalRecord? latestPR = null;

        foreach (PersonalRecord record in records)
        {
            if (record.UpdatedAt != null && record.Weight != null)
            {
                if (latestPR == null || record.UpdatedAt > latestPR.UpdatedAt)
                {
                    latestPR = record;
                }
            }
        }

        if (latestPR == null)
        {
            LatestPRLabel.Text = "N/A";
            LatestPRExerciseLabel.Text = "No records";
            return;
        }

        LatestPRLabel.Text = $"{latestPR.Weight} lbs";

        if (latestPR.ExerciseId == Guid.Parse("576d09f7-9686-45a2-b3ba-e8753219e0b3"))
        {
            LatestPRExerciseLabel.Text = "Clean";
        }
        else if (latestPR.ExerciseId == Guid.Parse("45db5851-46a2-4711-a0c0-5d307cd9967d"))
        {
            LatestPRExerciseLabel.Text = "Back Squat";
        }
        else if (latestPR.ExerciseId == Guid.Parse("d9036c3b-b69d-4945-b6d9-13f2b0e97edc"))
        {
            LatestPRExerciseLabel.Text = "Bench Press";
        }
        else
        {
            LatestPRExerciseLabel.Text = "Other exercise";
        }
    }
    public MetricsPage()
    {
        InitializeComponent();
        _personalRecordService = new PersonalRecordService(new PersonalRecordRepository());
        _routineEntryService = new RoutineEntryService(new RoutineEntryRepository());
        BindingContext = this;
    }

    // When a item from the drop down is selected this method is triggered and
    // fills in the weights depending on which exercise was selected calls 
    // update chart method
    private void ExercisePicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_personalRecordService == null)
        {
            return;
        }

        Guid exerciseId;

        if (ExercisePicker.SelectedIndex == 0)
        {
            exerciseId = Guid.Parse("d9036c3b-b69d-4945-b6d9-13f2b0e97edc");
        }
        else if (ExercisePicker.SelectedIndex == 1)
        {
            exerciseId = Guid.Parse("45db5851-46a2-4711-a0c0-5d307cd9967d");
        }
        else if (ExercisePicker.SelectedIndex == 2)
        {
            exerciseId = Guid.Parse("576d09f7-9686-45a2-b3ba-e8753219e0b3");
        }
        else
        {
            return;
        }

        var records = _personalRecordService.PersonalRecords
            .Where(pr => pr.ExerciseId == exerciseId
                && pr.Weight.HasValue
                && pr.UpdatedAt.HasValue)
            .OrderBy(pr => pr.UpdatedAt)
            .ToList();

        double[] weights = records
            .Select(pr => pr.Weight.Value)
            .ToArray();

        string[] dates = records
            .Select(pr => pr.UpdatedAt.Value.ToString("MM/dd"))
            .ToArray();

        UpdateChart(weights, dates);
    }

    // This method takes in the weight points for the chart and uses those to make a 
    // line then sets the xasis as custom month names. The y axis is automatically 
    // made based off the values in the LineSeries
    private void UpdateChart(double[] weights, string[] dates)
    {
        var accentColor = (Color)Application.Current.Resources["TextAccent"];
        var chartColor = SKColor.Parse(accentColor.ToHex());
        StrengthChart.Series = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = weights,
                Stroke = new SolidColorPaint(chartColor, 3),
                GeometryFill = new SolidColorPaint(chartColor),
                GeometryStroke = new SolidColorPaint(chartColor),
                Fill = null,
            }
        };

        StrengthChart.XAxes = new Axis[]
        {
            new Axis
            {
                Name = "Date",
                Labels = dates
            }
        };

        StrengthChart.YAxes = new Axis[]
        {
            new Axis
            {
                Name = "Weight (lbs)"
            }
        };
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
