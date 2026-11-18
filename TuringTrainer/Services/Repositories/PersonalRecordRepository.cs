using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public class PersonalRecordRepository : IPersonalRecordRepository
{
    private readonly Supabase.Client _client = Database.getClient();

    public ObservableCollection<PersonalRecord> PersonalRecords { get; private set; } = new();
    public async Task SelectPersonalRecords(Guid userId)
    {
        var response = await _client
            .From<PersonalRecord>()
            .Where(pr => pr.UserId == userId)
            .Get();

        PersonalRecords.Clear();

        foreach (var record in response.Models)
        {
            PersonalRecords.Add(record);
        }
    }
}
