using TuringTrainer.BusinessLogic;
using TuringTrainer.LeaderBoard;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer;
//Name: Bakary

/*
    * Name : Bakary Ceesay
    * This is the code-behind file for the Homepage.xaml page.
    * It contains the logic for handling user interactions on the Homepage page.
    */

public partial class Homepage : ContentPage
{
    private static readonly Guid CoachId = Guid.Parse("4daa0811-e8c6-431a-b4f8-21b05ce2d78f");
    private readonly IClassRepository _classRepository = new ClassRepository();

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
    private readonly TuringTrainer.Services.StudentImportService _studentImport = new();
    private readonly IUserService _userService;

    public Homepage()
    {
        _userService = new UserService(new UserRepository());
        
        InitializeComponent();
        BindableLayout.SetItemsSource(ClassListLayout, _classRepository.Classes);
        LoadActiveUser();

        //fill the class with atheletes
        ClassPicker.Items.Add("Class 1");
        ClassPicker.Items.Add("Class 2");
        ClassPicker.Items.Add("Class 3");
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            await _classRepository.SelectClassesByCoachAsync(CoachId);
            int totalClassCount = classes.Count + _classRepository.Classes.Count;
            ClassCountLabel.Text = $"{totalClassCount} {(totalClassCount == 1 ? "class" : "classes")}";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load classes: {ex.Message}", "OK");
        }
    }

    private async Task LoadActiveUser()
    {
        try
        {
            User? currentUser = await _userService.CurrentUser();

            

            if (currentUser != null)
            {
                ActiveUserIdLabel.Text = "Hello, " + currentUser.FirstName;
            }
            else
            {
                ActiveUserIdLabel.Text = "No session";
            }
        }
        catch
        {
            ActiveUserIdLabel.Text = "Not signed in";
        }
    }

    private async void OnClass1Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ClassIPage));
    }

    private async void OnClass2Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ClassIIPage));
    }

    private async void OnClass3Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ClassIIIPage));
    }

    private async void OnAddClassClicked(object sender, EventArgs e)
    {
        // Open Add Class flow
        await Shell.Current.GoToAsync(nameof(AddClass));
    }

    /// <summary>
    /// Opens the Add Student popup
    /// Author: Aaditya Bhanadari
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnAddStudentClicked(object sender, EventArgs e)
    {
        ClassPicker.SelectedIndex = -1;
        StudentEmailEntry.Text = "";
        PreviewLabel.Text = "";

        ImportFileLabel.Text ="Use the first worksheet with an email column header.";
        AddStudentPopup.IsVisible = true;
    }

    private void OnPreviewClicked(object? sender, EventArgs e)
    {
        string emailText = StudentEmailEntry.Text ?? "";

        string[] emails = emailText.Split(
            new[] { '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries);

        ShowPreview(emails);
    }

    private void ShowPreview(IEnumerable<string> emails)
    {
        if (ClassPicker.SelectedIndex == -1)
        {
            PreviewLabel.Text = "Please select a class.";
            return;
        }

        string selectedClassName =
            ClassPicker.Items[ClassPicker.SelectedIndex];

        List<string> results = _studentImport.Validate(emails);

        int validCount = 0;

        foreach (string result in results)
        {
            if (result.StartsWith("Valid: "))
            {
                validCount++;
            }
        }

        string details;

        if (results.Count == 0)
        {
            details = "Enter at least one email.";
        }
        else
        {
            details = string.Join("\n", results);
        }

        PreviewLabel.Text =
            selectedClassName + ": " + validCount + " valid email(s).\n" +
            "Preview only. No students were enrolled.\n" +
            details;
    }

    private async void OnUploadExcelClicked(object? sender, EventArgs e)
    {
        if (ClassPicker.SelectedIndex == -1)
        {
            PreviewLabel.Text = "Please select a class.";
            return;
        }

        try
        {
            var file = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Choose an Excel workbook (.xlsx)"
            });

            if (file == null)
            {
                return;
            }

            string extension = Path.GetExtension(file.FileName);

            if (!extension.Equals(
                ".xlsx",
                StringComparison.OrdinalIgnoreCase))
            {
                PreviewLabel.Text = "Please select an .xlsx workbook.";
                return;
            }

            using var stream = await file.OpenReadAsync();
            using var buffer = new MemoryStream();

            byte[] chunk = new byte[81920];
            int count;

            while ((count = await stream.ReadAsync(chunk)) > 0)
            {
                if (buffer.Length + count > 10 * 1024 * 1024)
                {
                    PreviewLabel.Text =
                        "Please choose a workbook smaller than 10 MB.";

                    return;
                }

                await buffer.WriteAsync(chunk.AsMemory(0, count));
            }

            buffer.Position = 0;

            List<string> emails = _studentImport.ReadExcel(buffer);

            StudentEmailEntry.Text = string.Join("\n", emails);
            ImportFileLabel.Text = file.FileName;

            ShowPreview(emails);
        }
        catch (Exception)
        {
            PreviewLabel.Text =
                "Could not read the workbook. Use an .xlsx file under " +
                "10 MB, with an email header and at most 5,000 rows.";
        }
    }

    private void OnPreviewInputChanged(object? sender, EventArgs e)
    {
        if (PreviewLabel != null)
        {
            PreviewLabel.Text = "";
        }
    }

    private void CloseAddStudentPopup(object sender, EventArgs e)
    {
        AddStudentPopup.IsVisible = false;
    }

    private async void OnReportsClicked(object sender, EventArgs e)
    {
        // Open Reports page
        await Shell.Current.GoToAsync(nameof(Reports));
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
    private async void OnNotificationsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Pages.NotificationsPage));
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
