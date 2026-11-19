using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public class ClassStudentRepository : IClassStudentRepository
{
    private readonly Supabase.Client _client = Database.getClient();

    public ObservableCollection<User> ClassMembers { get; private set; } = new();

    public async Task SelectStudentsByClass(string classId)
    {
        try
        {
            ClassMembers.Clear();

            if (!Guid.TryParse(classId, out Guid classGuid))
            {
                return;
            }

            var classStudents = await _client.From<ClassStudent>().Where(cs => cs.ClassId == classGuid).Get();

            var studentIds = new HashSet<Guid>();
            foreach (var classStudent in classStudents.Models)
            {
                studentIds.Add(classStudent.StudentId);
            }

            var userResponse = await _client.From<User>().Get();
            foreach (var user in userResponse.Models)
            {
                if (studentIds.Contains(user.UserId))
                {
                    ClassMembers.Add(user);
                }
            }
        }
        catch (Exception ex)
        {
            ClassMembers.Clear();
            Console.WriteLine("Error fetching class students: " + ex.Message);
        }
    }
}
