using System.Collections.ObjectModel;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.BusinessLogic;

public class PersonalRecordService : IPersonalRecordService
{
    private readonly IPersonalRecordRepository _personalRecordRepository;

    public PersonalRecordService(IPersonalRecordRepository personalRecordRepository)
    {
        _personalRecordRepository = personalRecordRepository;
    }

    public ObservableCollection<PersonalRecord> PersonalRecords
    {
        get { return _personalRecordRepository.PersonalRecords; }
    }
    public async Task SelectPersonalRecords(Guid userId)
    {
        if(userId == Guid.Empty)
        {
            return;
        }
        await _personalRecordRepository.SelectPersonalRecords(userId);
    }
}