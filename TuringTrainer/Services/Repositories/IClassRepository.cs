using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public interface IClassRepository
{
    ObservableCollection<Class> Classes { get; }
    Task SelectClassesByCoachAsync(Guid coachId);
    Task<Class> AddClassAsync(Class newClass);
}