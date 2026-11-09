using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public interface IExerciseService
{
    Task SelectAllExercises();
    Task<Exercise?> SelectExercise(Guid exerciseId);
    Task<ExerciseAdditionError> InsertExercise(Exercise exercise);
    Task<ExerciseEditError> UpdateExercise(Exercise exercise, string name);
    Task<ExerciseDeletionError> DeleteExercise(Exercise exercise);
}