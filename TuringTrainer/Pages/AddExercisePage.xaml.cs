using System.Collections.ObjectModel;
using TuringTrainer.BusinessLogic;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.Pages;
//Name: Luke Johnson

public partial class AddExercisePage : ContentPage
{
    private readonly IExerciseService _exerciseService = new ExerciseService(new ExerciseRepository());
    private readonly List<SelectableExercise> _allExercises = new();

    public event Action<List<string>>? Picked;

    public AddExercisePage()
    {
        InitializeComponent();

        LibraryList.ItemsSource = FilteredExercises;
    }

    public ObservableCollection<SelectableExercise> FilteredExercises { get; } = new();

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _exerciseService.SelectAllExercises();

        if (_allExercises.Count == 0)
        {
            foreach (Exercise exercise in _exerciseService.Exercises)
            {
                _allExercises.Add(new SelectableExercise(exercise));
            }
        }

        ApplyFilter();
    }

    private void OnSearchTextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        string search = SearchEntry.Text ?? "";

        FilteredExercises.Clear();

        foreach (SelectableExercise exercise in _allExercises)
        {
            if (exercise.Name.StartsWith(search, StringComparison.OrdinalIgnoreCase))
            {
                FilteredExercises.Add(exercise);
            }
        }
    }

    private void OnExerciseTapped(object? sender, TappedEventArgs e)
    {
        if (e.Parameter is not SelectableExercise exercise)
            return;

        exercise.IsSelected = !exercise.IsSelected;

        UpdateAddButton();
    }

    private void UpdateAddButton()
    {
        int count = _allExercises.Count(exercise => exercise.IsSelected);

        AddSelectedButton.IsEnabled = count > 0;
        AddSelectedButton.Text = count == 1 ? "ADD EXERCISE" : "ADD EXERCISES";
    }

    private async void OnAddSelectedClicked(object? sender, EventArgs e)
    {
        List<string> names = new();

        foreach (SelectableExercise exercise in _allExercises)
        {
            if (exercise.IsSelected)
            {
                names.Add(exercise.Name);
            }
        }

        if (names.Count == 0)
            return;

        Picked?.Invoke(names);

        await Navigation.PopModalAsync();
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
