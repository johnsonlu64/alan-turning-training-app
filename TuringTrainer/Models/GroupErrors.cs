namespace TuringTrainer.Models;

public enum GroupAdditionError
{
	NoError,
	InvalidGroupId,
	InvalidCoachId,
	InvalidStudentList,
	AlreadyInGroup,
	DuplicateGroupId,
	DBAdditionError
}