using TuringTrainer.BusinessLogic;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.BusinessLogic;
public class SportService : ISportService
{
  private readonly ISportRepository _sportRepository;

    public SportService(ISportRepository sportRepository)
    {
        _sportRepository = sportRepository;
    }

    public async Task SelectAllSports()
    {
        await _sportRepository.SelectAllSports();
    }

    public async Task<Sport?> SelectSport(Guid sportId)
    {
        if (sportId == Guid.Empty)
        {
            return null;
        }

        return await _sportRepository.SelectSport(sportId);
    }

    public async Task<SportAdditionError> InsertSport(Sport sport)
    {
        if (sport.SportId == Guid.Empty)
        {
            return SportAdditionError.InvalidId;
        }

        if (string.IsNullOrWhiteSpace(sport.Name))
        {
            return SportAdditionError.InvalidName;
        }

        return await _sportRepository.InsertSport(sport);
    }

    public async Task<SportEditError> UpdateSport(Sport sport, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return SportEditError.InvalidFieldError;
        }

        return await _sportRepository.UpdateSport(sport, name);
    }

    public async Task<SportDeletionError> DeleteSport(Sport sport)
    {
        if (sport != null)
        {
            return await _sportRepository.DeleteSport(sport);
        }
        else
        {
            return SportDeletionError.SportNotFound;
        }
    }
}