using System.Collections.ObjectModel;
using TuringTrainer.BusinessLogic;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.BusinessLogic;
public class ExerciseService : IExerciseService
{
  private readonly IExerciseRepository _exerciseRepository;

  public ExerciseService(IExerciseRepository exerciseRepository)
  {
    _exerciseRepository = exerciseRepository;
  }

  public ObservableCollection<Exercise> Exercises => _exerciseRepository.Exercises;

  public async Task SelectAllExercises()
  {
    await _exerciseRepository.SelectAllExercises();
  }

  public async Task<Exercise?> SelectExercise(Guid exerciseId)
  {
    if (exerciseId == Guid.Empty)
    {
      return null;
    }

    return await _exerciseRepository.SelectExercise(exerciseId);
  }

  public async Task<ExerciseAdditionError> InsertExercise(Exercise exercise)
  {
    if (exercise.ExerciseId == Guid.Empty)
    {
      return ExerciseAdditionError.InvalidId;
    }

    if (string.IsNullOrWhiteSpace(exercise.Name))
    {
      return ExerciseAdditionError.InvalidName;
    }

    return await _exerciseRepository.InsertExercise(exercise);
  }

  public async Task<ExerciseEditError> UpdateExercise(Exercise exercise, string name)
  {
    if (string.IsNullOrWhiteSpace(name))
    {
      return ExerciseEditError.InvalidFieldError;
    }

    return await _exerciseRepository.UpdateExercise(exercise, name);
  }

  public async Task<ExerciseDeletionError> DeleteExercise(Exercise exercise)
  {
    if (exercise != null)
    {
      return await _exerciseRepository.DeleteExercise(exercise);
    }
    else
    {
      return ExerciseDeletionError.ExerciseNotFound;
    }
  }
}
