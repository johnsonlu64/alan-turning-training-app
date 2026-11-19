using System.Collections.ObjectModel;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.BusinessLogic;

public class RoutineService : IRoutineService
{
    private readonly IRoutineRepository _routineRepository;

    public RoutineService(IRoutineRepository routineRepository)
    {
        _routineRepository = routineRepository;
    }

    public ObservableCollection<Routine> ClassRoutines => _routineRepository.ClassRoutines;

    public async Task GetRoutinesInClass(string classId)
    {
        await _routineRepository.SelectRoutinesByClass(classId);
    }
}
