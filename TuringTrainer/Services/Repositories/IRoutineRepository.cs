using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public interface IRoutineRepository
{
    ObservableCollection<Routine> ClassRoutines { get; }
    Task SelectRoutinesByClass(string classId);
}
