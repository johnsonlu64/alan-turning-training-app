using System.Collections.ObjectModel;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;

namespace TuringTrainer.BusinessLogic;

public class ClassStudentService : IClassStudentService
{
    private readonly IClassStudentRepository _classStudentRepository;

    public ClassStudentService(IClassStudentRepository classStudentRepository)
    {
        _classStudentRepository = classStudentRepository;
    }

    public ObservableCollection<User> ClassMembers => _classStudentRepository.ClassMembers;

    public async Task GetStudentsInClass(string classId)
    {
        await _classStudentRepository.SelectStudentsByClass(classId);
    }
}
