using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public class ExerciseEntrySetRepository : IExerciseEntrySetRepository
{
    private readonly Supabase.Client _client = Database.getClient();

    public async Task<List<ExerciseEntrySet>> SelectExerciseEntrySets(Guid exerciseEntryId)
    {
        var response = await _client.From<ExerciseEntrySet>().Where(entry => entry.ExerciseEntryId == exerciseEntryId).Where(entry => entry.Reps == 1).Get();

        return response.Models;
    }
}