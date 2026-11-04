using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public interface ISportRepository
{

    public ObservableCollection<Sport> Sports { get; }
    public Task SelectAllSports();

    public Task SelectAllSportsByGroup(String groupId);
    public Task SelectAllSportsByClass(String classId);

    public Task<Sport?> SelectSport(Guid sportId);
    public Task<SportAdditionError> InsertSport(Sport sport);
    public Task<SportEditError> UpdateSport(Sport sportToUpdate, String name);
    public Task<SportDeletionError> DeleteSport(Sport sportToDelete);
}
