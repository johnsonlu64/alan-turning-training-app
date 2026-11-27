using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public interface IExerciseEntryService
{
    Task<List<ExerciseEntry>> SelectExerciseEntries(Guid routineEntryId, Guid exerciseId);
}