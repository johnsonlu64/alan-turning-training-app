using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public interface IExerciseRepository
{

    public ObservableCollection<Exercise> Exercises { get; }
    public Task SelectAllExercises();

    public Task<Exercise?> SelectExercise(Guid exerciseId);
    public Task<ExerciseAdditionError> InsertExercise(Exercise exercise);
    public Task<ExerciseEditError> UpdateExercise(Exercise exerciseToUpdate, String name);
    public Task<ExerciseDeletionError> DeleteExercise(Exercise exerciseToDelete);
}