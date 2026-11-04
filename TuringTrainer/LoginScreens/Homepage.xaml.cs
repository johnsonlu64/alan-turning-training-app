using System.Collections.ObjectModel;
using TuringTrainer.BusinessLogic;
using TuringTrainer.LeaderBoard;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer;

public partial class Homepage : ContentPage
{
    private readonly IUserService _userService = new UserService(new UserRepository());
    private readonly ClassRepository _classRepository = new();
    private readonly Dictionary<Guid, List<Athlete>> _athletesByClass = new();

    private ObservableCollection<ClassListItem> Classes { get; } = new();

    public Homepage()
    {
        InitializeComponent();
        BindableLayout.SetItemsSource(ClassesLayout, Classes);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadActiveUserAsync();
        await LoadClassesAsync();
    }

    private async Task LoadActiveUserAsync()
    {
        try
        {
            User? currentUser = await _userService.CurrentUser();
            ActiveUserIdLabel.Text = currentUser == null
                ? "No session"
                : $"Hello, {currentUser.FirstName}";
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Unable to load current user: {ex}");
            ActiveUserIdLabel.Text = "Unable to load user";
        }
    }

    private async Task LoadClassesAsync()
    {
        if (!Guid.TryParse(_userService.CurrentUserId(), out Guid coachId))
        {
            Classes.Clear();
            UpdateClassListUi();
            await DisplayAlertAsync("Sign in required", "Sign in to view your classes.", "OK");
            return;
        }

        try
        {
            IReadOnlyList<TrainingClass> classes =
                await _classRepository.GetClassesForCoachAsync(coachId);

            Classes.Clear();
            for (int index = 0; index < classes.Count; index++)
            {
                Classes.Add(new ClassListItem(classes[index], index + 1));
            }

            UpdateClassListUi();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Unable to load classes", ex.Message, "OK");
        }
    }

    private void UpdateClassListUi()
    {
        ClassCountLabel.Text = Classes.Count == 1
            ? "1 class"
            : $"{Classes.Count} classes";
        NoClassesLabel.IsVisible = Classes.Count == 0;
        ClassPicker.ItemsSource = Classes.Select(classItem => classItem.Name).ToList();
    }

    private async void OnClassTapped(object? sender, TappedEventArgs e)
    {
        if (sender is BindableObject bindable &&
            bindable.BindingContext is ClassListItem classItem)
        {
            await OpenClassAsync(classItem.TrainingClass);
        }
    }

    private async void OnClassOpenClicked(object? sender, EventArgs e)
    {
        if (sender is Button { CommandParameter: ClassListItem classItem })
        {
            await OpenClassAsync(classItem.TrainingClass);
        }
    }

    private static Task OpenClassAsync(TrainingClass trainingClass)
    {
        return Shell.Current.GoToAsync(
            nameof(ClassDetailsPage),
            new Dictionary<string, object> { ["Class"] = trainingClass });
    }

    private async void OnAddClassClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddClass));
    }

    private async void OnAddStudentClicked(object? sender, EventArgs e)
    {
        if (Classes.Count == 0)
        {
            await DisplayAlertAsync("No classes", "Create a class before adding students.", "OK");
            return;
        }

        ClassPicker.SelectedIndex = 0;
        StudentIdEntry.Text = string.Empty;
        AddStudentPopup.IsVisible = true;
    }

    private void OnStudentIdEntryCompleted(object? sender, EventArgs e)
    {
        int selectedIndex = ClassPicker.SelectedIndex;
        string studentId = StudentIdEntry.Text?.Trim() ?? string.Empty;
        if (selectedIndex < 0 || selectedIndex >= Classes.Count ||
            string.IsNullOrWhiteSpace(studentId))
        {
            return;
        }

        TrainingClass selectedClass = Classes[selectedIndex].TrainingClass;
        if (!_athletesByClass.TryGetValue(selectedClass.ClassId, out List<Athlete>? athletes))
        {
            athletes = new List<Athlete>();
            _athletesByClass.Add(selectedClass.ClassId, athletes);
        }

        athletes.Add(new Athlete { Name = studentId, Points = 0 });
        AddStudentPopup.IsVisible = false;
    }

    private void CloseAddStudentPopup(object? sender, EventArgs e)
    {
        AddStudentPopup.IsVisible = false;
    }

    private async void OnReportsClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Reports));
    }

    private void OnHomeClicked(object? sender, EventArgs e)
    {
    }

    private async void OnLeaderboardClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LeaderBoardPage));
    }

    private async void OnNotificationsClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Pages.NotificationsPage));
    }

    private async void OnMetricsClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MetricsPage));
    }

    private async void OnSettingsClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SettingsPage");
    }
}
