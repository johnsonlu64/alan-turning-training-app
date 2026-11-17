using Microsoft.Maui.Storage;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.BusinessLogic;

public class CurrentOrganizationService : ICurrentOrganizationService
{
    private const string PreferenceKeyPrefix = "current_organization_id:";

    private readonly IUserService _userService;
    private readonly IOrganizationRepository _organizationRepository;

    public CurrentOrganizationService(IUserService userService, IOrganizationRepository organizationRepository)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
    }

    public CurrentOrganizationService()
        : this(new UserService(new UserRepository()), new OrganizationRepository())
    {
    }

    public async Task<Guid?> GetCurrentOrganizationId()
    {
        Guid? userId = GetCurrentUserId();
        if (userId == null)
        {
            return null;
        }

        List<Organization> organizations = await GetOrganizationsForUserAsync(userId.Value);
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

        return await _organizationRepository.SelectOrganization(organizationId.Value);
    }

    public async Task<bool> SetCurrentOrganizationId(Guid organizationId)
    {
        if (organizationId == Guid.Empty)
        {
            return false;
        }

        Guid? userId = GetCurrentUserId();
        if (userId == null)
        {
            return false;
        }

        List<Organization> organizations = await GetOrganizationsForUserAsync(userId.Value);
        if (!organizations.Any(o => o.OrganizationId == organizationId))
        {
            return false;
        }

        WriteCurrentOrganizationId(userId.Value, organizationId);
        return true;
    }

    public Task ClearCurrentOrganization(Guid? userId = null)
    {
        Guid? targetUserId = userId ?? GetCurrentUserId();
        if (targetUserId != null)
        {
            RemoveCurrentOrganizationId(targetUserId.Value);
        }

        return Task.CompletedTask;
    }

    private Guid? GetCurrentUserId()
    {
        string? userId = _userService.CurrentUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return null;
        }

        return Guid.TryParse(userId, out Guid parsedUserId) ? parsedUserId : null;
    }

    private async Task<List<Organization>> GetOrganizationsForUserAsync(Guid userId)
    {
        await _organizationRepository.SelectOrganizationsByUser(userId);
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
