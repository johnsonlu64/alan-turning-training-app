using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public interface IClassStudentService
{
    ObservableCollection<User> ClassMembers { get; }
    Task GetStudentsInClass(string classId);
}
