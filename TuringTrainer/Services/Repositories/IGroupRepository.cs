using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public interface IGroupRepository
{
	Task SelectAllGroupsByCoach(Guid coachId);
	Task<Group?> SelectGroup(Guid groupId);
	Task SelectStudentsByGroup(Guid groupId);
	Task<GroupAdditionError> InsertStudentsIntoGroup(Guid groupId, IList<Guid> studentIds);
}