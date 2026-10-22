using System.Collections.ObjectModel;
//Name: Luke Johnson

namespace TuringTrainer.Pages;

/// <summary>
/// The routine editor. The exercises and sets are literal content, but the percent and
/// the reps of a set can be changed and the load follows.
/// </summary>
public partial class RoutineBuilderPage : ContentPage, IQueryAttributable
{
    /// <summary>The exercises of the open routine, in order.</summary>
    private ObservableCollection<BuilderExercise> _exercises = new ObservableCollection<BuilderExercise>();

    /// <summary>The exercise shown in the editor on the right.</summary>
    private BuilderExercise? _selected;

    public RoutineBuilderPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Receives the routine id from the Shell query string. The Shell calls this on every
    /// navigation, so the page always shows the routine that was asked for.
    /// </summary>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        string routineId = "lowerBody";
        if (query.ContainsKey("routineId"))
            routineId = (string)query["routineId"];

        Show(routineId);
    }

    /// <summary>Loads a routine and shows it.</summary>
    private void Show(string routineId)
    {
        _exercises = ExercisesFor(routineId);

        Routine? routine = Routine.All.FirstOrDefault(r => r.Id == routineId);
        Header.Title = routine?.Title ?? "Routine";
        Header.RightLabel = routine?.ExerciseCount ?? "";

        ExerciseList.ItemsSource = _exercises;
        ExerciseList.SelectedItem = _exercises[0];
    }

    private void OnExerciseSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        _selected = (BuilderExercise)e.CurrentSelection[0];

        ExerciseNameLabel.Text = _selected.Name;
        SetList.ItemsSource = _selected.Sets;
        SetList.SelectedItem = _selected.Sets.Count > 0 ? _selected.Sets[0] : null;
    }

    private void OnSetSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        BuilderSet set = (BuilderSet)e.CurrentSelection[0];
        PercentLabel.Text = set.PercentText;
    }

    private void OnPercentUp(object? sender, EventArgs e)
    {
        BuilderSet? set = SelectedSet(sender);
        if (set == null)
            return;

        set.Percent = Math.Min(set.Percent + 5, 100);
        PercentLabel.Text = set.PercentText;
    }

    private void OnPercentDown(object? sender, EventArgs e)
    {
        BuilderSet? set = SelectedSet(sender);
        if (set == null)
            return;

        set.Percent = Math.Max(set.Percent - 5, 0);
        PercentLabel.Text = set.PercentText;
    }

    private void OnRepsUp(object? sender, EventArgs e)
    {
        BuilderSet? set = SelectedSet(sender);
        if (set == null)
            return;

        set.Reps = set.Reps + 1;
    }

    private void OnRepsDown(object? sender, EventArgs e)
    {
        BuilderSet? set = SelectedSet(sender);
        if (set == null)
            return;

        set.Reps = Math.Max(set.Reps - 1, 1);
    }

    private void OnAddSetClicked(object? sender, EventArgs e)
    {
        if (_selected == null)
            return;

        BuilderSet set = new BuilderSet
        {
            Label = "SET " + (_selected.Sets.Count + 1),
            BaseLoad = _selected.BaseLoad,
            Percent = 70,
            Reps = 5,
        };
        set.UpdateLoad();

        _selected.Sets.Add(set);
        SetList.SelectedItem = set;
    }

    private void OnRemoveSetClicked(object? sender, EventArgs e)
    {
        if (_selected == null || _selected.Sets.Count == 0)
            return;

        BuilderSet set = (BuilderSet)SetList.SelectedItem;
        _selected.Sets.Remove(set);
    }

    private void OnRemoveExerciseClicked(object? sender, EventArgs e)
    {
        if (_selected == null)
            return;

        _exercises.Remove(_selected);
        ExerciseList.SelectedItem = _exercises.Count > 0 ? _exercises[0] : null;
    }

    private async void OnAddExerciseClicked(object? sender, EventArgs e)
    {
        AddExercisePage page = new AddExercisePage();
        page.Picked += AddExercise;

        await Navigation.PushModalAsync(page);
    }

    private void AddExercise(string name)
    {
        BuilderExercise exercise = new BuilderExercise
        {
            Index = (_exercises.Count + 1).ToString("00"),
            Name = name,
            Focus = "NEWLY ADDED",
            BaseLoad = 100,
        };

        exercise.Sets.Add(NewSet(exercise, 1));

        _exercises.Add(exercise);
        ExerciseList.SelectedItem = exercise;
    }

    private async void OnChangeAnchorClicked(object? sender, EventArgs e)
    {
        AnchorPickerPage page = new AnchorPickerPage();
        page.Picked += SetAnchor;

        await Navigation.PushModalAsync(page);
    }

    private void SetAnchor(string name)
    {
        if (_selected == null)
            return;

        AnchorNameLabel.Text = name;
        AnchorDetailLabel.Text = "LINKED TO " + name;
    }

    private void OnSaveClicked(object? sender, EventArgs e)
    {
        // The builder is a visual mock-up, so saving changes nothing.
    }

    /// <summary>The set that a button belongs to.</summary>
    private BuilderSet? SelectedSet(object? sender)
    {
        Button? button = sender as Button;
        if (button?.CommandParameter is BuilderSet set)
            return set;

        return SetList.SelectedItem as BuilderSet;
    }

    /// <summary>A new set for an exercise.</summary>
    private static BuilderSet NewSet(BuilderExercise exercise, int number)
    {
        BuilderSet set = new BuilderSet
        {
            Label = "SET " + number,
            BaseLoad = exercise.BaseLoad,
            Percent = 70,
            Reps = 5,
        };
        set.UpdateLoad();

        return set;
    }

    /// <summary>The exercises of a routine, hard-coded per routine.</summary>
    /// <param name="routineId">Stable routine id, for example "upperBody".</param>
    private static ObservableCollection<BuilderExercise> ExercisesFor(string routineId)
    {
        ObservableCollection<BuilderExercise> exercises = new ObservableCollection<BuilderExercise>();

        if (routineId == "upperBody")
        {
            exercises.Add(Exercise("01", "Bench Press", "PRIMARY 90% OF 1RM", 202.5, 70, 5, 75, 5));
            exercises.Add(Exercise("02", "Overhead Press", "LINKED TO BENCH PRESS", 202.5, 70, 5));
            exercises.Add(Exercise("03", "Barbell Row", "LINKED TO BENCH PRESS", 202.5, 75, 6));
        }
        else if (routineId == "fullBody")
        {
            exercises.Add(Exercise("01", "Power Clean", "PRIMARY 90% OF 1RM", 139.5, 60, 3, 70, 3));
            exercises.Add(Exercise("02", "Hang Snatch", "LINKED TO POWER CLEAN", 139.5, 70, 3));
            exercises.Add(Exercise("03", "Front Squat", "PRIMARY 75% OF 1RM", 206.3, 75, 4));
        }
        else
        {
            exercises.Add(Exercise("01", "Back Squat", "PRIMARY 90% OF 1RM", 283.5, 70, 5, 80, 5));
            exercises.Add(Exercise("02", "Front Squat", "LINKED TO BACK SQUAT", 283.5, 80, 5));
            exercises.Add(Exercise("03", "Romanian Deadlift", "LINKED TO BACK SQUAT", 283.5, 70, 8));
        }

        return exercises;
    }

    /// <summary>One exercise with its sets.</summary>
    private static BuilderExercise Exercise(
        string index, string name, string focus, double baseLoad, params int[] sets)
    {
        BuilderExercise exercise = new BuilderExercise
        {
            Index = index,
            Name = name,
            Focus = focus,
            BaseLoad = baseLoad,
        };

        for (int i = 0; i < sets.Length; i += 2)
        {
            BuilderSet set = new BuilderSet
            {
                Label = "SET " + (i / 2 + 1),
                BaseLoad = baseLoad,
                Percent = sets[i],
                Reps = sets[i + 1],
            };
            set.UpdateLoad();

            exercise.Sets.Add(set);
        }

        return exercise;
    }
}
