using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public interface IOrganizationRepository
{
    ObservableCollection<Organization> Organizations { get; }
    ObservableCollection<User> OrganizationMembers { get; }

    Task SelectAllOrganizations();
    Task SelectOrganizationsByUser(Guid userId);
    Task<Organization?> SelectOrganization(Guid organizationId);
    Task SelectMembersByOrganization(Guid organizationId);
    Task<OrganizationAdditionError> InsertOrganization(Organization organization);
    Task<OrganizationAdditionError> InsertUsersIntoOrganization(Guid organizationId, IList<Guid> userIds);
}
