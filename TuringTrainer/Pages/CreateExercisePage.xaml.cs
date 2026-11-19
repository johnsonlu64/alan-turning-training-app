using TuringTrainer.BusinessLogic;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.Pages;

public partial class CreateExercisePage : ContentPage
{
    private readonly IExerciseService _exerciseService = new ExerciseService(new ExerciseRepository());

    public event Action<string>? Created;

    public bool CloseAsModal { get; set; }

    public CreateExercisePage()
    {
        InitializeComponent();
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        await CloseAsync();
    }

    private void OnNameCompleted(object? sender, EventArgs e)
    {
        OnCreateClicked(sender, e);
    }

    private async void OnCreateClicked(object? sender, EventArgs e)
    {
        string name = NameEntry.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(name))
        {
            await DisplayAlertAsync("Name Required", "Enter a name for the exercise.", "OK");
            return;
        }

        Exercise exercise = new Exercise(Guid.NewGuid(), name);

        ExerciseAdditionError result = await _exerciseService.InsertExercise(exercise);

        if (result != ExerciseAdditionError.NoError)
        {
            await DisplayAlertAsync("Could Not Create Exercise", MessageFor(result), "OK");
            return;
        }

        Created?.Invoke(name);
        NameEntry.Text = string.Empty;

        await CloseAsync();
    }

    private async Task CloseAsync()
    {
        if (CloseAsModal || Navigation.ModalStack.Contains(this))
        {
            await Navigation.PopModalAsync();
            return;
        }

        await Shell.Current.GoToAsync("..");
    }

    private static string MessageFor(ExerciseAdditionError error)
    {
        return error switch
        {
            ExerciseAdditionError.DuplicateExerciseId => "That exercise already exists in your library.",
            ExerciseAdditionError.InvalidName => "The exercise name is not valid.",
            _ => "The exercise could not be saved. Try again.",
        };
    }
}
