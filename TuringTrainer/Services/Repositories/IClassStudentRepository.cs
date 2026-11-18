using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public interface IClassStudentRepository
{
    ObservableCollection<User> ClassMembers { get; }
    Task SelectStudentsByClass(string classId);
}
