using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public interface IPersonalRecordService
{
    ObservableCollection<PersonalRecord> PersonalRecords { get; }
    Task SelectPersonalRecords(Guid userId);
}