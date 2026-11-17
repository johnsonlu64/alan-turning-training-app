using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public interface IOrganizationService
{
    Task SelectAllOrganizations();
    Task SelectOrganizationsByUser(Guid userId);
    Task<Organization?> SelectOrganization(Guid organizationId);
    Task SelectMembersByOrganization(Guid organizationId);
    Task<OrganizationAdditionError> InsertOrganization(string organizationName);
    Task<OrganizationAdditionError> InsertUsersIntoOrganization(Guid organizationId, IList<Guid> userIds);
}
