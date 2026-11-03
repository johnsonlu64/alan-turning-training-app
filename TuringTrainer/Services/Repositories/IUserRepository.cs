using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.Services.Repositories;

public interface IUserRepository
{

    public ObservableCollection<User> Users { get; }
    public Task SelectAllUsers();

    public Task SelectAllUsersByGroup(String groupId);
    public Task SelectAllUsersByClass(String classId);
    public Task SelectAllUsersBySport(String sportId);

    public Supabase.Gotrue.Session? GetUserSession();
    public Supabase.Gotrue.User? GetUser();

    public Task<bool> SignUpUser(string email, string password);
    public Task<bool> SignInUser(string email, string password);
    public Task LogoutUser();

    public Task<User?> SelectUser(Guid userId);
    public Task<UserAdditionError> InsertUser(User user);
    public Task<UserEditError> UpdateUser(User currentUser, String firstName, String middleInitial, String lastName, bool isCoach);
    public Task<UserDeletionError> DeleteUser(User user);
}
