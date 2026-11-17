using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.BusinessLogic;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task SelectAllOrganizations()
    {
        await _organizationRepository.SelectAllOrganizations();
    }

    public async Task SelectOrganizationsByUser(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return;
        }

        await _organizationRepository.SelectOrganizationsByUser(userId);
    }

    public async Task<Organization?> SelectOrganization(Guid organizationId)
    {
        if (organizationId == Guid.Empty)
        {
            return null;
        }

        return await _organizationRepository.SelectOrganization(organizationId);
    }

    public async Task SelectMembersByOrganization(Guid organizationId)
    {
        if (organizationId == Guid.Empty)
        {
            return;
        }

        await _organizationRepository.SelectMembersByOrganization(organizationId);
    }

    public async Task<OrganizationAdditionError> InsertOrganization(string organizationName)
    {
        if (string.IsNullOrWhiteSpace(organizationName))
        {
            return OrganizationAdditionError.InvalidOrganizationName;
        }

        Organization organization = new Organization(Guid.NewGuid(), organizationName.Trim());

        return await _organizationRepository.InsertOrganization(organization);
    }

    public async Task<OrganizationAdditionError> InsertUsersIntoOrganization(
        Guid organizationId, IList<Guid> userIds)
    {
        if (organizationId == Guid.Empty)
        {
            return OrganizationAdditionError.InvalidOrganizationId;
        }

        List<Guid> validIds = new List<Guid>();

        if (userIds == null)
        {
            return OrganizationAdditionError.InvalidUserList;
        }

        foreach (var id in userIds)
        {
            if (id != Guid.Empty && !validIds.Contains(id))
            {
                validIds.Add(id);
            }
        }

        if (validIds.Count == 0)
        {
            return OrganizationAdditionError.InvalidUserList;
        }

        return await _organizationRepository.InsertUsersIntoOrganization(organizationId, validIds);
    }
}
