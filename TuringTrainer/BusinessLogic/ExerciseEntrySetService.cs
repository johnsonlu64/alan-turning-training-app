using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.BusinessLogic;

public class ExerciseEntrySetService : IExerciseEntrySetService
{
    private readonly IExerciseEntrySetRepository _exerciseEntrySetRepository;

    public ExerciseEntrySetService(IExerciseEntrySetRepository exerciseEntrySetRepository)
    {
        _exerciseEntrySetRepository = exerciseEntrySetRepository;
    }

    public async Task<List<ExerciseEntrySet>> SelectExerciseEntrySets(Guid exerciseEntryId)
    {
        if (exerciseEntryId == Guid.Empty)
        {
            return new List<ExerciseEntrySet>();
        }

        return await _exerciseEntrySetRepository.SelectExerciseEntrySets(exerciseEntryId);
    }
}