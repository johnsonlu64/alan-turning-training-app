using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public interface IPersonalRecordRepository
{
    public ObservableCollection<PersonalRecord> PersonalRecords { get; }
    Task SelectPersonalRecords(Guid userId);
    
}
