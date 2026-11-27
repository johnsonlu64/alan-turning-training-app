using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public class ExerciseEntryRepository : IExerciseEntryRepository
{
    private readonly Supabase.Client _client = Database.getClient();

    public async Task<List<ExerciseEntry>> SelectExerciseEntries(Guid routineEntryId, Guid exerciseId)
    {
        var response = await _client.From<ExerciseEntry>().Where(entry => entry.RoutineEntryId == routineEntryId).Where(entry => entry.ExerciseId == exerciseId).Get();

        return response.Models;
    }
}