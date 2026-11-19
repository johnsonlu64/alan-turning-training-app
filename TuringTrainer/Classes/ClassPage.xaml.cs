using TuringTrainer.BusinessLogic;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer;

[QueryProperty(nameof(SelectedClassId), "classId")]
[QueryProperty(nameof(ClassName), "className")]
public partial class ClassPage : ContentPage, IQueryAttributable
{
    private readonly ISportService _sportService;
    private readonly IClassStudentService _classStudentService;
    private readonly IRoutineService _routineService;
    private bool _loaded;

    private string _selectedClassId = string.Empty;
    public string SelectedClassId
    {
        get => _selectedClassId;
        set => _selectedClassId = value;
    }

    private string _className = string.Empty;
    public string ClassName
    {
        get => _className;
        set => _className = value;
    }

    public ClassPage()
    {
        InitializeComponent();

        _sportService = new SportService(new SportRepository());
        _classStudentService = new ClassStudentService(new ClassStudentRepository());
        _routineService = new RoutineService(new RoutineRepository());
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("classId", out object? idValue) && idValue != null)
        {
            SelectedClassId = idValue.ToString() ?? string.Empty;
        }

        if (query.TryGetValue("className", out object? nameValue) && nameValue != null)
        {
            ClassName = Uri.UnescapeDataString(nameValue.ToString() ?? string.Empty);
        }

        _ = LoadOnceAsync();
    }

    private bool HasValidClassId => Guid.TryParse(SelectedClassId, out _);

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadOnceAsync();
    }

    private async Task LoadOnceAsync()
    {
        if (_loaded || !HasValidClassId)
        {
            return;
        }

        _loaded = true;

        try
        {
            await LoadAsync();
        }
        catch (Exception ex)
        {
            _loaded = false;
            await DisplayAlert("Error", $"Could not load class data: {ex.Message}", "OK");
        }
    }

    private async Task LoadAsync()
    {
        ClassNameLabel.Text = ClassName ?? "Class";

        await Task.WhenAll(
            _sportService.SelectAllSportsByClass(SelectedClassId),
            _classStudentService.GetStudentsInClass(SelectedClassId),
            _routineService.GetRoutinesInClass(SelectedClassId));

        // Members are students enrolled directly in this class via class_students, resolved as User objects.
        SportsList.ItemsSource = _sportService.Sports;
        MembersList.ItemsSource = _classStudentService.ClassMembers;

        var displayRoutines = new List<Pages.Routine>();
        int position = 1;
        foreach (Models.Routine routine in _routineService.ClassRoutines)
        {
            displayRoutines.Add(new Pages.Routine
            {
                Id = routine.RoutineId.ToString(),
                Index = position.ToString("00"),
                Title = routine.Name,
            });
            position++;
        }
        RoutinesList.ItemsSource = displayRoutines;

        ShowSports();
    }

    private void OnSportClicked(object? sender, EventArgs e)
    {
        // TODO: open sport detail
    }

    private async void OnMemberClicked(object sender, EventArgs e)
    {
        var clickedElement = sender as VisualElement;
        if (clickedElement == null) return;

        var member = clickedElement.BindingContext as Models.User;
        if (member == null) return;

        string memberName = $"{member.FirstName} {member.LastName}";
        await Shell.Current.GoToAsync($"{nameof(MetricsPage)}?name={Uri.EscapeDataString(memberName)}");
    }

    private void OnSportsTabClicked(object? sender, EventArgs e)
    {
        ShowSports();
    }

    private void OnRoutinesTabClicked(object? sender, EventArgs e)
    {
        ShowRoutines();
    }

    private void OnMembersTabClicked(object? sender, EventArgs e)
    {
        ShowMembers();
    }

    private void ShowSports()
    {
        SportsTab.BackgroundColor = ResourceColor("AccentTint", Colors.Gray);
        SportsTab.TextColor = ResourceColor("TextAccent", Colors.White);
        RoutinesTab.BackgroundColor = Colors.Transparent;
        RoutinesTab.TextColor = ResourceColor("TextMuted", Colors.Gray);
        MembersTab.BackgroundColor = Colors.Transparent;
        MembersTab.TextColor = ResourceColor("TextMuted", Colors.Gray);

        SportsList.IsVisible = true;
        RoutinesList.IsVisible = false;
        MembersList.IsVisible = false;
    }

    private void ShowRoutines()
    {
        SportsTab.BackgroundColor = Colors.Transparent;
        SportsTab.TextColor = ResourceColor("TextMuted", Colors.Gray);
        RoutinesTab.BackgroundColor = ResourceColor("AccentTint", Colors.Gray);
        RoutinesTab.TextColor = ResourceColor("TextAccent", Colors.White);
        MembersTab.BackgroundColor = Colors.Transparent;
        MembersTab.TextColor = ResourceColor("TextMuted", Colors.Gray);

        SportsList.IsVisible = false;
        RoutinesList.IsVisible = true;
        MembersList.IsVisible = false;
    }

    private void ShowMembers()
    {
        SportsTab.BackgroundColor = Colors.Transparent;
        SportsTab.TextColor = ResourceColor("TextMuted", Colors.Gray);
        RoutinesTab.BackgroundColor = Colors.Transparent;
        RoutinesTab.TextColor = ResourceColor("TextMuted", Colors.Gray);

        MembersTab.BackgroundColor = ResourceColor("AccentTint", Colors.Gray);
        MembersTab.TextColor = ResourceColor("TextAccent", Colors.White);

        SportsList.IsVisible = false;
        RoutinesList.IsVisible = false;
        MembersList.IsVisible = true;
    }

    private static Color ResourceColor(string key, Color fallback)
    {
        if (Application.Current?.Resources.TryGetValue(key, out object value) == true && value is Color color)
        {
            return color;
        }

        return fallback;
    }

    private async void OnRoutineSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
        {
            return;
        }

        if (e.CurrentSelection[0] is not Pages.Routine routine)
        {
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(Pages.RoutineBuilderPage)}?routineId={routine.Id}");
        RoutinesList.SelectedItem = null;
    }
}
