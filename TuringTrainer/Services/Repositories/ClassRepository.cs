using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public class ClassRepository
{
    private readonly Supabase.Client _client = Database.getClient();

    public async Task<IReadOnlyList<TrainingClass>> GetClassesForCoachAsync(Guid coachId)
    {
        var response = await _client
            .From<TrainingClass>()
            .Where(trainingClass => trainingClass.CoachId == coachId)
            .Get();

        return response.Models;
    }

    public async Task<TrainingClass> CreateClassAsync(string name, Guid coachId)
    {
        var trainingClass = new TrainingClass
        {
            ClassId = Guid.NewGuid(),
            CoachId = coachId,
            Name = name
        };

        await _client.From<TrainingClass>().Insert(trainingClass);
        return trainingClass;
    }
}