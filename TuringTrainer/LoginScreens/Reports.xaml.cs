using TuringTrainer.BusinessLogic;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer;
/*
 * Name : Bakary Ceesay
 * This is the code-behind file for the Reports.xaml page.
 * It loads the coach's classes and the sports, collects the filters,
 * and opens the results page with the generated report.
 */

public partial class Reports : ContentPage
{
    private const string AllClasses = "All Classes";
    private const string AllSports = "All Sports";

    private readonly IUserService _userService = new UserService(new UserRepository());
    private readonly ClassRepository _classRepository = new();
    private readonly SportRepository _sportRepository = new();
    private readonly ReportRepository _reportRepository = new();

    // Picker index 0 is the "All ..." entry, so item n of these lists is picker index n + 1.
    private List<Class> _classes = new();
    private List<Sport> _sports = new();
    private bool _loaded;

    public Reports()
    {
        InitializeComponent();

        StartDatePicker.Date = DateTime.Today.AddDays(-30);
        EndDatePicker.Date = DateTime.Today;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_loaded)
            return;

        try
        {
            if (Guid.TryParse(_userService.CurrentUserId(), out Guid coachId))
            {
                await _classRepository.SelectClassesByCoachAsync(coachId);
                _classes = _classRepository.Classes.ToList();
            }

            await _sportRepository.SelectAllSports();
            _sports = _sportRepository.Sports.ToList();

            FillPicker(ClassPicker, AllClasses, _classes.Select(c => c.Name));
            FillPicker(CustomClassPicker, AllClasses, _classes.Select(c => c.Name));
            FillPicker(SportPicker, AllSports, _sports.Select(s => s.Name));
            FillPicker(CustomSportPicker, AllSports, _sports.Select(s => s.Name));

            _loaded = true;
        }
        catch (Exception ex)
        {
            ShowStatus("Could not load classes and sports: " + ex.Message);
        }
    }

    private static void FillPicker(Picker picker, string allText, IEnumerable<string> names)
    {
        List<string> items = new() { allText };
        items.AddRange(names);
        picker.ItemsSource = items;
        picker.SelectedIndex = 0;
    }

    private async void OnGenerateClicked(object? sender, EventArgs e)
    {
        StatusLabel.IsVisible = false;

        DateOnly start = DateOnly.FromDateTime(StartDatePicker.Date ?? DateTime.Today);
        DateOnly end = DateOnly.FromDateTime(EndDatePicker.Date ?? DateTime.Today);

        if (start > end)
        {
            ShowStatus("The start date must be on or before the end date.");
            return;
        }

        // Which pickers apply depends on the chosen report type.
        Picker classPicker = CustomRadio.IsChecked ? CustomClassPicker : ClassPicker;
        Picker sportPicker = CustomRadio.IsChecked ? CustomSportPicker : SportPicker;

        Guid? classId = null;
        Guid? sportId = null;
        List<string> titleParts = new();

        if (ByClassRadio.IsChecked || CustomRadio.IsChecked)
        {
            int index = classPicker.SelectedIndex;
            if (index > 0 && index - 1 < _classes.Count)
            {
                classId = _classes[index - 1].ClassId;
                titleParts.Add(_classes[index - 1].Name);
            }
        }

        if (BySportRadio.IsChecked || CustomRadio.IsChecked)
        {
            int index = sportPicker.SelectedIndex;
            if (index > 0 && index - 1 < _sports.Count)
            {
                sportId = _sports[index - 1].SportId;
                titleParts.Add(_sports[index - 1].Name);
            }
        }

        string title = titleParts.Count == 0 ? "All Athletes" : string.Join(" • ", titleParts);

        SetBusy(true);
        try
        {
            ReportResult report = await _reportRepository.BuildReportAsync(title, classId, sportId, start, end);

            await Shell.Current.GoToAsync(nameof(ReportResultsPage), new Dictionary<string, object>
            {
                ["report"] = report,
            });
        }
        catch (Exception ex)
        {
            ShowStatus("Could not generate the report: " + ex.Message);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void SetBusy(bool busy)
    {
        GenerateButton.IsEnabled = !busy;
        Spinner.IsVisible = busy;
        Spinner.IsRunning = busy;
    }

    private void ShowStatus(string message)
    {
        StatusLabel.Text = message;
        StatusLabel.IsVisible = true;
    }
}
