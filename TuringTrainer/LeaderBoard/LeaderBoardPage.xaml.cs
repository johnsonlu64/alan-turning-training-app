using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TuringTrainer.LeaderBoard;

namespace TuringTrainer;

public partial class LeaderBoardPage : ContentPage
{
    private readonly LeaderboardService service = new();
    private LeaderboardOptions options = new();

    private bool updating;
    private bool busy;
    private bool editing;
    private bool pageVisible;
    private bool optionsLoaded;
    private int requestVersion;

    public LeaderBoardPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        pageVisible = true;

        if (!optionsLoaded)
            await LoadOptionsAsync();
        else if (TryGetSelection(out _, out _, out _))
            await LoadRowsAsync();
        else
            UpdateControls();
    }

    protected override void OnDisappearing()
    {
        pageVisible = false;
        ++requestVersion;
        base.OnDisappearing();
    }

    private async Task LoadOptionsAsync()
    {
        int version = ++requestVersion;

        updating = true;
        optionsLoaded = false;

        Board.ItemsSource = null;
        ClassPicker.ItemsSource = null;
        GroupPicker.ItemsSource = null;
        ExercisePicker.ItemsSource = null;

        SetBusy(true);
        StatusLabel.Text = "Loading classes…";

        try
        {
            var result = await service.GetOptionsAsync();

            if (!IsCurrentRequest(version))
                return;

            options = result;
            ClassPicker.ItemsSource = options.Classes;
            optionsLoaded = true;

            StatusLabel.Text = options.Classes.Count == 0
                ? "No classes available for this coach account."
                : "Select a class.";
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);

            if (IsCurrentRequest(version))
            {
                StatusLabel.Text = "Could not load classes. Sign in and tap Refresh.";
            }
        }
        finally
        {
            updating = false;

            if (IsCurrentRequest(version))
                SetBusy(false);
        }
    }

    private void OnClassChanged(object sender, EventArgs e)
    {
        if (updating)
            return;

        ClearRows();
        updating = true;

        try
        {
            GroupPicker.ItemsSource = null;
            ExercisePicker.ItemsSource = null;

            if (ClassPicker.SelectedItem is not LeaderboardChoice selected)
                return;

            var groups = options.Groups
                .Where(g => g.ClassId == selected.Id)
                .OrderBy(g => g.Name)
                .ToList();

            GroupPicker.ItemsSource = groups;

            StatusLabel.Text = groups.Count == 0
                ? "No group memberships found for this class."
                : "Select a group.";
        }
        finally
        {
            updating = false;
            UpdateControls();
        }
    }

    private void OnGroupChanged(object sender, EventArgs e)
    {
        if (updating)
            return;

        ClearRows();
        updating = true;

        try
        {
            ExercisePicker.ItemsSource = null;

            if (GroupPicker.SelectedItem is not LeaderboardChoice selected)
                return;

            var exercises = options.Exercises
                .Where(x => x.GroupId == selected.Id)
                .OrderBy(x => x.Name)
                .ToList();

            ExercisePicker.ItemsSource = exercises;

            StatusLabel.Text = exercises.Count == 0
                ? "No exercises assigned to this group."
                : "Select an exercise.";
        }
        finally
        {
            updating = false;
            UpdateControls();
        }
    }

    private async void OnExerciseChanged(object sender, EventArgs e)
    {
        if (!updating)
            await LoadRowsAsync();
    }

    private async Task LoadRowsAsync()
    {
        if (!TryGetSelection(
                out Guid classId,
                out Guid groupId,
                out Guid exerciseId))
        {
            return;
        }

        int version = ++requestVersion;

        Board.ItemsSource = null;
        SetBusy(true);
        StatusLabel.Text = "Loading rankings…";

        try
        {
            var result = await service.GetRowsAsync(
                classId,
                groupId,
                exerciseId);

            if (!IsCurrentRequest(version))
                return;

            var rows = result
                .OrderByDescending(x => x.Ratio.HasValue)
                .ThenByDescending(x => x.Ratio)
                .ThenBy(x => x.Name)
                .ThenBy(x => x.AthleteId)
                .ToList();

            for (int i = 0; i < rows.Count; i++)
            {
                if (!rows[i].Ratio.HasValue)
                {
                    rows[i].Rank = null;
                }
                else if (i > 0 && rows[i].Ratio == rows[i - 1].Ratio)
                {
                    rows[i].Rank = rows[i - 1].Rank;
                }
                else
                {
                    rows[i].Rank = i + 1;
                }
            }

            Board.ItemsSource = rows;

            StatusLabel.Text = rows.Count == 0
                ? "No athletes found."
                : $"{rows.Count} athletes · All-time PRs";
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);

            if (IsCurrentRequest(version))
            {
                StatusLabel.Text ="Could not load rankings. Tap Refresh to retry.";
            }
        }
        finally
        {
            if (IsCurrentRequest(version))
                SetBusy(false);
        }
    }

    private async void OnUpdateWeightClicked(object sender, EventArgs e)
    {
        if (busy || editing ||
            sender is not Button { BindingContext: LeaderboardRow row } ||
            !TryGetSelection(
                out Guid classId,
                out Guid groupId,
                out Guid exerciseId))
        {
            return;
        }

        editing = true;
        UpdateControls();

        try
        {
            decimal? weight = await AskWeightAsync(
                "Update current body weight",
                $"{row.Name}\nEnter current body weight in pounds.",
                row.CurrentWeightLb,
                1500);

            if (!weight.HasValue || !pageVisible)
                return;

            await SaveChangeAsync(
                classId,
                groupId,
                row.AthleteId,
                exerciseId,
                "current_weight",
                weight.Value);
        }
        finally
        {
            editing = false;
            UpdateControls();
        }
    }

    private async void OnPrOptionsClicked(object sender, EventArgs e)
    {
        if (busy || editing ||
            sender is not Button { BindingContext: LeaderboardRow row } ||
            !TryGetSelection(
                out Guid classId,
                out Guid groupId,
                out Guid exerciseId))
        {
            return;
        }

        editing = true;
        UpdateControls();

        try
        {
            string action = await DisplayActionSheetAsync(row.Name, "Cancel", null, "Record new PR", "Set body weight at existing PR");

            if (!pageVisible)
                return;

            if (action == "Record new PR")
            {
                decimal? prWeight = await AskWeightAsync(
                    "Record new PR",
                    $"{row.Name}\nEnter the lifting weight in pounds.",
                    null,
                    10000);

                if (!prWeight.HasValue || !pageVisible)
                    return;

                if (row.PrWeightLb.HasValue &&
                    (double)prWeight.Value <= row.PrWeightLb.Value)
                {
                    await DisplayAlertAsync("Higher PR needed", "The new PR must exceed the existing PR.", "OK");

                    return;
                }

                decimal? bodyWeight = await AskWeightAsync("Body weight at PR", "Enter the athlete's body weight when this PR was achieved. ",null, 1500);

                if (!bodyWeight.HasValue || !pageVisible)
                    return;

                await SaveChangeAsync(
                    classId,
                    groupId,
                    row.AthleteId,
                    exerciseId,
                    "new_pr",
                    prWeight.Value,
                    bodyWeight.Value);
            }
            else if (action == "Set body weight at existing PR")
            {
                if (row.PrWeightLb is null || row.PrWeightLb <= 0)
                {
                    await DisplayAlertAsync("No PR", "Record a PR for this exercise first.", "OK");
                    return;
                }

                decimal? bodyWeight = await AskWeightAsync(
                    "Body weight at existing PR",
                    $"{row.Name}\nEnter body weight when the "
                    + $"{row.PrWeightLb:0.##} lb PR was achieved. "
                    + "This changes that PR's ratio.",
                    row.PrBodyWeightLb,
                    1500);

                if (!bodyWeight.HasValue || !pageVisible)
                    return;

                await SaveChangeAsync(
                    classId,
                    groupId,
                    row.AthleteId,
                    exerciseId,
                    "pr_body_weight",
                    bodyWeight.Value);
            }
        }
        finally
        {
            editing = false;
            UpdateControls();
        }
    }

    private async Task<decimal?> AskWeightAsync(
        string title,
        string message,
        decimal? initialValue,
        decimal maximum)
    {
        string text = await DisplayPromptAsync(
            title,
            message,
            accept: "Continue",
            cancel: "Cancel",
            placeholder: "Weight in lb",
            keyboard: Keyboard.Numeric,
            initialValue: initialValue?.ToString(
                "0.###",
                CultureInfo.CurrentCulture));

        if (text == null)
            return null;

        if (!decimal.TryParse(
                text,
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowLeadingWhite |
                NumberStyles.AllowTrailingWhite,
                CultureInfo.CurrentCulture,
                out decimal weight) ||
            weight <= 0 ||
            weight > maximum)
        {
            await DisplayAlertAsync(
                "Invalid weight",
                $"Enter a number greater than 0 and no more than {maximum} lb.",
                "OK");

            return null;
        }

        return weight;
    }

    private async Task SaveChangeAsync(
        Guid classId,
        Guid groupId,
        Guid athleteId,
        Guid exerciseId,
        string action,
        decimal value,
        decimal? prBodyWeight = null)
    {
        SetBusy(true);

        try
        {
            await service.SaveAsync(
                classId,
                groupId,
                athleteId,
                exerciseId,
                action,
                value,
                prBodyWeight);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);

            if (pageVisible)
            {
                await DisplayAlertAsync("Could not save", "Check your connection and coach permissions. " + "A new PR must exceed the existing PR.", "OK");
            }

            return;
        }
        finally
        {
            SetBusy(false);
        }

        if (pageVisible)
        {
            await LoadRowsAsync();
        }
    }

    private bool TryGetSelection(
        out Guid classId,
        out Guid groupId,
        out Guid exerciseId)
    {
        classId = Guid.Empty;
        groupId = Guid.Empty;
        exerciseId = Guid.Empty;

        if (ClassPicker.SelectedItem is not LeaderboardChoice selectedClass ||
            GroupPicker.SelectedItem is not LeaderboardChoice selectedGroup ||
            ExercisePicker.SelectedItem is not LeaderboardChoice exercise)
        {
            return false;
        }

        classId = selectedClass.Id;
        groupId = selectedGroup.Id;
        exerciseId = exercise.Id;

        return true;
    }

    private bool IsCurrentRequest(int version)
    {
        return pageVisible && version == requestVersion;
    }

    private void ClearRows()
    {
        ++requestVersion;
        Board.ItemsSource = null;
    }

    private void SetBusy(bool value)
    {
        busy = value;

        LoadingIndicator.IsVisible = value;
        LoadingIndicator.IsRunning = value;

        UpdateControls();
    }

    private void UpdateControls()
    {
        bool enabled = !busy && !editing;

        ClassPicker.IsEnabled = enabled;

        GroupPicker.IsEnabled = enabled && GroupPicker.ItemsSource?.Count > 0;
        ExercisePicker.IsEnabled = enabled && ExercisePicker.ItemsSource?.Count > 0;

        Board.IsEnabled = enabled;
        RefreshButton.IsEnabled = enabled;
    }

    private async void OnRefreshClicked(object sender, EventArgs e)
    {
        if (busy || editing)
            return;

        if (TryGetSelection(out _, out _, out _))
            await LoadRowsAsync();
        else
            await LoadOptionsAsync();
    }

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        if (!busy && !editing)
            await Shell.Current.GoToAsync(nameof(Homepage));
    }

    private async void OnMetricsClicked(object sender, EventArgs e)
    {
        if (!busy && !editing)
            await Shell.Current.GoToAsync(nameof(ClassesPage));
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        if (!busy && !editing)
            await Shell.Current.GoToAsync("SettingsPage");
    }
}