using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public interface IRoutineService
{
    ObservableCollection<Routine> ClassRoutines { get; }
    Task GetRoutinesInClass(string classId);
}
