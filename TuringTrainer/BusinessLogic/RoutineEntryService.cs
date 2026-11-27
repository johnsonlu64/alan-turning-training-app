using TuringTrainer.Services.Repositories;
using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public class RoutineEntryService : IRoutineEntryService
{
    private readonly IRoutineEntryRepository _routineEntryRepository;

    public RoutineEntryService(IRoutineEntryRepository routineEntryRepository)
    {
        _routineEntryRepository = routineEntryRepository;
    }

    public async Task<int> SelectCompletedWorkouts(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return 0;
        }

        return await _routineEntryRepository.SelectCompletedWorkouts(userId);
    }

    public async Task<DateTime?> SelectLastWorkoutDate(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return null;
        }

        return await _routineEntryRepository.SelectLastWorkoutDate(userId);
    }

    public async Task<List<RoutineEntry>> SelectRoutineEntries(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return new List<RoutineEntry>();
        }

        return await _routineEntryRepository.SelectRoutineEntries(userId);
    }
}