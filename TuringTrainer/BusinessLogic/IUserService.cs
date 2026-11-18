using System.Collections.ObjectModel;
using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public interface IUserService
{
    ObservableCollection<User> Users { get; }
    ObservableCollection<User> ClassMembers { get; }
    Task SelectAllUsers();
    Task GetUsersInClass(string classId);
    Supabase.Gotrue.Session? GetUserSession();
    string? CurrentUserId();
    Task<User?> CurrentUser();
    Task<UserAdditionError> SignUpUser(string email, string password, string username);
    Task<UserSignInError> SignInUser(string email, string password);
    Task<UserSignoutError> SignOutUser();
    Task<UserEditError> UpdateUser(User user, string firstName, string middleInitial, string lastName, bool isCoach);
}