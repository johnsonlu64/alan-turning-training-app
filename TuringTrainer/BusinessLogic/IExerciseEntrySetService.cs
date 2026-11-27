using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public interface IExerciseEntrySetService
{
    Task<List<ExerciseEntrySet>> SelectExerciseEntrySets(Guid exerciseEntryId);
}