using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public class RoutineRepository : IRoutineRepository
{
    private readonly Supabase.Client _client = Database.getClient();

    public ObservableCollection<Routine> ClassRoutines { get; private set; } = new();

    public async Task SelectRoutinesByClass(string classId)
    {
        try
        {
            ClassRoutines.Clear();

            if (!Guid.TryParse(classId, out Guid classGuid))
            {
                return;
            }

            var response = await _client.From<Routine>().Where(routine => routine.ClassId == classGuid).Get();

            foreach (var routine in response.Models)
            {
                ClassRoutines.Add(routine);
            }
        }
        catch (Exception ex)
        {
            ClassRoutines.Clear();
            Console.WriteLine("Error fetching routines by class: " + ex.Message);
        }
    }
}
