using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public interface ICurrentOrganizationService
{
    Task<Guid?> GetCurrentOrganizationId();
    Task<Organization?> GetCurrentOrganization();
    Task<bool> SetCurrentOrganizationId(Guid organizationId);
    Task ClearCurrentOrganization(Guid? userId = null);
}
