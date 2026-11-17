using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public class ClassRepository : IClassRepository
{
    private readonly Supabase.Client _client = Database.getClient();

    public ObservableCollection<Class> Classes { get; } = new();

    public async Task SelectClassesByCoachAsync(Guid coachId)
    {
        var response = await _client
            .From<Class>()
            .Where(schoolClass => schoolClass.CoachId == coachId)
            .Get();

        Classes.Clear();
        foreach (var schoolClass in response.Models)
        {
            Classes.Add(schoolClass);
        }
    }

    public async Task<Class> AddClassAsync(Class newClass)
    {
        var response = await _client.From<Class>().Insert(newClass);
        var insertedClass = response.Models.FirstOrDefault();
        if (insertedClass is null)
        {
            throw new InvalidOperationException("The class was not returned after insertion.");
        }

        Classes.Add(insertedClass);
        return insertedClass;
    }
}