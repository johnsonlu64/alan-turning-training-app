using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public interface IGroupService 
{
	Task SelectAllGroupsByCoach(Guid coachId);
    Task<Group?> SelectGroup(Guid groupId);
    Task SelectStudentsByGroup(Guid groupId);
    Task<GroupAdditionError> InsertStudentsIntoGroup(Guid groupId, IList<Guid> studentIds);
}