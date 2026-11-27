using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public interface IExerciseEntrySetRepository
{
    Task<List<ExerciseEntrySet>> SelectExerciseEntrySets(Guid exerciseEntryId);
}