using TuringTrainer.Models;

namespace TuringTrainer.BusinessLogic;

public interface IUserService
{
    Supabase.Gotrue.Session? GetUserSession();
    string? CurrentUserId();
    Task<User?> CurrentUser();
    Task<UserAdditionError> SignUpUser(string email, string password, string username);
    Task<UserSignInError> SignInUser(string email, string password);
    Task<UserSignoutError> SignOutUser();
}