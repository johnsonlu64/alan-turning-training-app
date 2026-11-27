using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public interface IExerciseEntryRepository
{
    Task<List<ExerciseEntry>> SelectExerciseEntries(Guid routineEntryId, Guid exerciseId);
}