using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public interface IRoutineEntryService
{
    Task<int> SelectCompletedWorkouts(Guid userId);

    Task<DateTime?> SelectLastWorkoutDate(Guid userId);
    Task<List<RoutineEntry>> SelectRoutineEntries(Guid userId);
}