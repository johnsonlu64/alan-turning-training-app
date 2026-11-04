using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public interface ISportService
{
    Task SelectAllSports();
    Task<Sport?> SelectSport(Guid sportId);
    Task<SportAdditionError> InsertSport(Sport sport);
    Task<SportEditError> UpdateSport(Sport sport, string name);
    Task<SportDeletionError> DeleteSport(Sport sport);
}