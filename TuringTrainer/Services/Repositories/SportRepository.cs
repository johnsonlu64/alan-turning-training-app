using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public class SportRepository : ISportRepository
{

    private readonly Supabase.Client _client = Database.getClient();


    public ObservableCollection<Sport> Sports { get; private set; } = new();

    public async Task SelectAllSports()
    {
        try
        {
            var response = await _client.From<Sport>().Get();
            Sports.Clear();

            foreach (var sport in response.Models)
            {
                Sports.Add(sport);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching sports: {ex.Message}");
        }
    }

    public async Task SelectAllSportsByGroup(string groupId)
    {
        return;
    }

    public async Task SelectAllSportsByClass(string classId)
    {
        return;
    }

    public async Task<Sport?> SelectSport(Guid sportId)
    {
        var response = await _client.From<Sport>().Where(sport => sport.SportId == sportId).Get();
        if (response != null)
        {
            return response!.Models.FirstOrDefault();
        }
        return null;
    }

    public async Task<SportAdditionError> InsertSport(Sport sport)
    {
        try
        {
            var response = await _client.From<Sport>().Insert(sport);
            if (response.Models.Any())
            {
                Sports.Add(response.Models.First());
            }
            else
            {
                return SportAdditionError.DuplicateSportId;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Insert failed, {0}", ex);
            return ex.Message.Contains("already exists") ? SportAdditionError.DuplicateSportId : SportAdditionError.DBAdditionError;
        }
        return SportAdditionError.NoError;
    }

    public async Task<SportEditError> UpdateSport(Sport sportToUpdate, String name)
    {
        try
        {
            sportToUpdate.Name = name;
            var response = await _client.From<Sport>().Update(sportToUpdate);
            if (response.Models.Any())
            {
                var updatedSport = response.Models.First();
                var index = Sports.IndexOf(sportToUpdate);
                if (index >= 0)
                {
                    Sports[index] = updatedSport;
                }
            }
            else
            {
                return SportEditError.SportNotFound;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Update failed, {0}", ex);
            return SportEditError.DBEditError;
        }
        return SportEditError.NoError;
    }

    public async Task<SportDeletionError> DeleteSport(Sport sportToDelete)
    {
        try
        {
            var response = await _client.From<Sport>().Delete(sportToDelete);
            if (response.Models.Any())
            {
                Console.WriteLine($"{Sports.Count}");
                Sports.Remove(sportToDelete);
                Console.WriteLine($"{Sports.Count}");
            }
            else
            {
                return SportDeletionError.SportNotFound;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Delete failed, {0}", ex);
            return SportDeletionError.DBDeletionError;
        }
        return SportDeletionError.NoError;
    }
}
