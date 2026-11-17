using Microsoft.Maui.Storage;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.BusinessLogic;

public class OrganizationService : IOrganizationService
{
    private const string PreferenceKeyPrefix = "current_organization_id:";

    private readonly IUserService _userService;
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(IUserService userService, IOrganizationRepository organizationRepository)
    {
        _userService = userService;
        _organizationRepository = organizationRepository;
    }

    public OrganizationService()
        : this(new UserService(new UserRepository()), new OrganizationRepository())
    {
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

    public async Task<Guid?> GetCurrentOrganizationId()
    {
        Guid? userId = _userService.CurrentUserId();
        if (userId == null)
        {
            return null;
        }

        List<Organization> organizations = await GetOrganizationsForUser(userId.Value);
        if (organizations.Count == 0)
        {
            RemoveCurrentOrganizationId(userId.Value);
            return null;
        }

        Guid? storedOrganizationId = ReadCurrentOrganizationId(userId.Value);
        if (storedOrganizationId.HasValue && organizations.Any(o => o.OrganizationId == storedOrganizationId.Value))
        {
            return storedOrganizationId.Value;
        }

        if (organizations.Count == 1)
        {
            Guid organizationId = organizations[0].OrganizationId;
            WriteCurrentOrganizationId(userId.Value, organizationId);
            return organizationId;
        }

        RemoveCurrentOrganizationId(userId.Value);
        return null;
    }

    public async Task<Organization?> GetCurrentOrganization()
    {
        Guid? organizationId = await GetCurrentOrganizationId();
        if (organizationId == null)
        {
            return null;
        }

        Organization? cachedOrganization = _organizationRepository.Organizations
            .FirstOrDefault(o => o.OrganizationId == organizationId.Value);
        if (cachedOrganization != null)
        {
            return cachedOrganization;
        }

        return await SelectOrganization(organizationId.Value);
    }

    public async Task<bool> SetCurrentOrganizationId(Guid organizationId)
    {
        if (organizationId == Guid.Empty)
        {
            return false;
        }

        Guid? userId = _userService.CurrentUserId();
        if (userId == null)
        {
            return false;
        }

        List<Organization> organizations = await GetOrganizationsForUser(userId.Value);
        if (!organizations.Any(o => o.OrganizationId == organizationId))
        {
            return false;
        }

        WriteCurrentOrganizationId(userId.Value, organizationId);
        return true;
    }

    public Task ClearCurrentOrganization(Guid? userId = null)
    {
        Guid? targetUserId = userId ?? _userService.CurrentUserId();
        if (targetUserId != null)
        {
            RemoveCurrentOrganizationId(targetUserId.Value);
        }

        return Task.CompletedTask;
    }

    private async Task<List<Organization>> GetOrganizationsForUser(Guid userId)
    {
        await SelectOrganizationsByUser(userId);
        return _organizationRepository.Organizations.ToList();
    }

    private static string GetPreferenceKey(Guid userId)
    {
        return PreferenceKeyPrefix + userId.ToString("N");
    }

    private static Guid? ReadCurrentOrganizationId(Guid userId)
    {
        string? storedValue = Preferences.Get(GetPreferenceKey(userId), null);
        if (string.IsNullOrWhiteSpace(storedValue))
        {
            return null;
        }

        return Guid.TryParse(storedValue, out Guid organizationId) ? organizationId : null;
    }

    private static void WriteCurrentOrganizationId(Guid userId, Guid organizationId)
    {
        Preferences.Set(GetPreferenceKey(userId), organizationId.ToString("N"));
    }

    private static void RemoveCurrentOrganizationId(Guid userId)
    {
        Preferences.Remove(GetPreferenceKey(userId));
    }
}
