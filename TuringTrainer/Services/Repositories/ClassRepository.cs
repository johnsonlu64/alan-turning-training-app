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

        if (newClass == null)
            throw new ArgumentNullException(nameof(newClass));

        if (newClass.CoachId == Guid.Empty || newClass.OrganizationId == Guid.Empty)
            throw new ArgumentException("Coach and organization are required.");

        if (string.IsNullOrWhiteSpace(newClass.Name))
            throw new ArgumentException("Class name is required.");

        if (newClass.ClassId == Guid.Empty)
            newClass.ClassId = Guid.NewGuid();

        newClass.Name = newClass.Name.Trim();

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