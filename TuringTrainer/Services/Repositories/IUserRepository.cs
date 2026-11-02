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

    public Task<User?> SelectUser(String userId);
    public Task<UserAdditionError> InsertUser(User user);
    public Task<UserEditError> UpdateUser(User currentUser, String title, int year, int numStars);
    public Task<UserDeletionError> DeleteUser(User user);
}