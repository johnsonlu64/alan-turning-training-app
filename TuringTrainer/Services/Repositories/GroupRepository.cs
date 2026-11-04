using System.Collections.ObjectModel;
using TuringTrainer.Models;
using static Supabase.Postgrest.Constants;

namespace TuringTrainer.Services.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly Supabase.Client _client = Database.getClient();

    public ObservableCollection<Group> Groups { get; private set; } = new();
    public ObservableCollection<User> GroupMembers { get; private set; } = new();


    public async Task SelectAllGroupsByCoach(Guid coachId)
    {
        try
        {
            var response = await _client.From<Group>().Get();

            Groups.Clear();

            foreach (var group in response.Models)
            {
                if (group.CoachId == coachId)
                {
                    Groups.Add(group);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching groups: " + ex.Message);
        }
    }

    public async Task<Group?> SelectGroup(Guid groupId)
    {
        var response = await _client.From<Group>().Get();

        foreach (var group in response.Models)
        {
            if (group.GroupId == groupId)
            {
                return group;
            }
        }

        return null;
    }

    public async Task SelectStudentsByGroup(Guid groupId)
    {
        try
        {
            var memberships = await _client.From<UserInGroup>().Get();

            GroupMembers.Clear();

            foreach (var membership in memberships.Models)
            {
                if (membership.GroupId == groupId)
                {
                    var userResponse = await _client.From<User>().Get();

                    foreach (var user in userResponse.Models)
                    {
                        if (user.UserId == membership.UserId)
                        {
                            GroupMembers.Add(user);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching group members: " + ex.Message);
        }
    }

    public async Task<GroupAdditionError> InsertStudentsIntoGroup(
        Guid groupId, IList<Guid> studentIds)
    {
        try
        {
            var memberships = await _client.From<UserInGroup>().Get();

            List<UserInGroup> rows = new List<UserInGroup>();

            foreach (var studentId in studentIds)
            {
                bool alreadyInGroup = false;

                foreach (var membership in memberships.Models)
                {
                    if (membership.GroupId == groupId &&
                        membership.UserId == studentId)
                    {
                        alreadyInGroup = true;
                    }
                }

                if (!alreadyInGroup && studentId != Guid.Empty)
                {
                    rows.Add(new UserInGroup(studentId, groupId));
                }
            }

            if (rows.Count == 0)
            {
                return GroupAdditionError.AlreadyInGroup;
            }

            await _client.From<UserInGroup>().Insert(rows);

            return GroupAdditionError.NoError;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Insert failed: " + ex.Message);

            if (ex.Message.Contains("already exists"))
            {
                return GroupAdditionError.DuplicateGroupId;
            }

            return GroupAdditionError.DBAdditionError;
        }
    }
}
