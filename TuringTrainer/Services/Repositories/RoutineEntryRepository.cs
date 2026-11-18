using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public class RoutineEntryRepository : IRoutineEntryRepository
{
    private readonly Supabase.Client _client = Database.getClient();

    public async Task<int> SelectCompletedWorkouts(Guid userId)
    {
        var response = await _client.From<RoutineEntry>().Where(entry => entry.UserId == userId).Where(entry => entry.Status == "completed").Get();
        return response.Models.Count;
    }

    public async Task<DateTime?> SelectLastWorkoutDate(Guid userId)
    {
        var response = await _client.From<RoutineEntry>().Where(entry => entry.UserId == userId).Where(entry => entry.Status == "completed").Get();

        DateTime? lastWorkout = null;
        foreach (RoutineEntry entry in response.Models)
        {
            if (lastWorkout == null || entry.EndTime > lastWorkout)
            {
                lastWorkout = entry.EndTime;
            }
        }

        return lastWorkout;
    }
}