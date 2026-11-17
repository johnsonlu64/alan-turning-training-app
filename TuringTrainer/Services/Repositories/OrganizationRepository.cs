using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly Supabase.Client _client = Database.getClient();

    public ObservableCollection<Organization> Organizations { get; private set; } = new();
    public ObservableCollection<User> OrganizationMembers { get; private set; } = new();

    public async Task SelectAllOrganizations()
    {
        try
        {
            var response = await _client.From<Organization>().Get();

            Organizations.Clear();

            foreach (var organization in response.Models)
            {
                Organizations.Add(organization);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching organizations: " + ex.Message);
        }
    }

    public async Task SelectOrganizationsByUser(Guid userId)
    {
        try
        {
            var memberships = await _client.From<UserInOrganization>().Get();

            List<Guid> organizationIds = new List<Guid>();

            foreach (var membership in memberships.Models)
            {
                if (membership.UserId == userId)
                {
                    organizationIds.Add(membership.OrganizationId);
                }
            }

            var response = await _client.From<Organization>().Get();

            Organizations.Clear();

            foreach (var organization in response.Models)
            {
                if (organizationIds.Contains(organization.OrganizationId))
                {
                    Organizations.Add(organization);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching organizations for user: " + ex.Message);
        }
    }

    public async Task<Organization?> SelectOrganization(Guid organizationId)
    {
        try
        {
            var response = await _client.From<Organization>()
                .Where(organization => organization.OrganizationId == organizationId)
                .Get();

            return response.Models.FirstOrDefault();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching organization: " + ex.Message);
            return null;
        }
    }

    public async Task SelectMembersByOrganization(Guid organizationId)
    {
        try
        {
            var memberships = await _client.From<UserInOrganization>().Get();

            var userResponse = await _client.From<User>().Get();

            OrganizationMembers.Clear();

            foreach (var membership in memberships.Models)
            {
                if (membership.OrganizationId != organizationId)
                {
                    continue;
                }

                foreach (var user in userResponse.Models)
                {
                    if (user.UserId == membership.UserId)
                    {
                        OrganizationMembers.Add(user);
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching organization members: " + ex.Message);
        }
    }

    public async Task<OrganizationAdditionError> InsertOrganization(Organization organization)
    {
        try
        {
            var response = await _client.From<Organization>().Insert(organization);

            if (response.Models.Any())
            {
                Organizations.Add(response.Models.First());
            }
            else
            {
                return OrganizationAdditionError.DuplicateOrganizationId;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Insert failed: " + ex.Message);

            if (ex.Message.Contains("already exists"))
            {
                return OrganizationAdditionError.DuplicateOrganizationId;
            }

            return OrganizationAdditionError.DBAdditionError;
        }

        return OrganizationAdditionError.NoError;
    }

    public async Task<OrganizationAdditionError> InsertUsersIntoOrganization(
        Guid organizationId, IList<Guid> userIds)
    {
        try
        {
            var memberships = await _client.From<UserInOrganization>().Get();

            List<UserInOrganization> rows = new List<UserInOrganization>();

            foreach (var userId in userIds)
            {
                bool alreadyInOrganization = false;

                foreach (var membership in memberships.Models)
                {
                    if (membership.OrganizationId == organizationId &&
                        membership.UserId == userId)
                    {
                        alreadyInOrganization = true;
                    }
                }

                if (!alreadyInOrganization && userId != Guid.Empty)
                {
                    rows.Add(new UserInOrganization(userId, organizationId));
                }
            }

            if (rows.Count == 0)
            {
                return OrganizationAdditionError.AlreadyInOrganization;
            }

            await _client.From<UserInOrganization>().Insert(rows);

            return OrganizationAdditionError.NoError;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Insert failed: " + ex.Message);

            if (ex.Message.Contains("already exists"))
            {
                return OrganizationAdditionError.DuplicateOrganizationId;
            }

            return OrganizationAdditionError.DBAdditionError;
        }
    }
}
