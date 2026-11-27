using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.BusinessLogic;

public class ExerciseEntryService : IExerciseEntryService
{
    private readonly IExerciseEntryRepository _exerciseEntryRepository;

    public ExerciseEntryService(IExerciseEntryRepository exerciseEntryRepository)
    {
        _exerciseEntryRepository = exerciseEntryRepository;
    }

    public async Task<List<ExerciseEntry>> SelectExerciseEntries(Guid routineEntryId, Guid exerciseId)
    {
        if (routineEntryId == Guid.Empty || exerciseId == Guid.Empty)
        {
            return new List<ExerciseEntry>();
        }

        return await _exerciseEntryRepository.SelectExerciseEntries(routineEntryId, exerciseId);
    }
}