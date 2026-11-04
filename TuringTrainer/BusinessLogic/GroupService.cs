using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.BusinessLogic;

public class GroupService : IGroupService
{
	private readonly IGroupRepository _groupRepository;
	public GroupService(IGroupRepository groupRepository)
	{
		_groupRepository = groupRepository;
	}
	
	public async Task SelectAllGroupsByCoach(Guid coachId)
    {
        if (coachId == Guid.Empty)
        {
            return;
        }

        await _groupRepository.SelectAllGroupsByCoach(coachId);
    }

    public async Task<Group?> SelectGroup(Guid groupId)
    {
        if (groupId == Guid.Empty)
        {
            return null;
        }

        return await _groupRepository.SelectGroup(groupId);
    }

    public async Task SelectStudentsByGroup(Guid groupId)
    {
        if (groupId == Guid.Empty)
        {
            return;
        }

        await _groupRepository.SelectStudentsByGroup(groupId);
    }

    public async Task<GroupAdditionError> InsertStudentsIntoGroup(
        Guid groupId, IList<Guid> studentIds)
    {
        if (groupId == Guid.Empty)
        {
            return GroupAdditionError.InvalidGroupId;
        }

        List<Guid> validIds = new List<Guid>();

        if (studentIds == null)
        {
            return GroupAdditionError.InvalidStudentList;
        }

        foreach (var id in studentIds)
        {
            if (id != Guid.Empty && !validIds.Contains(id))
            {
                validIds.Add(id);
            }
        }

        if (validIds.Count == 0)
        {
            return GroupAdditionError.InvalidStudentList;
        }

        return await _groupRepository.InsertStudentsIntoGroup(groupId, validIds);
    }
}