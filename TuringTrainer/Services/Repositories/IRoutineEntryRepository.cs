using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public interface IRoutineEntryRepository
{
    Task<int> SelectCompletedWorkouts(Guid userId);
    Task<DateTime?> SelectLastWorkoutDate(Guid userId);
    Task<List<RoutineEntry>> SelectRoutineEntries(Guid userId);
}