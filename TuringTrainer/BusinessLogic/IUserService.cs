using TuringTrainer.Models;
using Supabase.Gotrue;

namespace TuringTrainer.BusinessLogic;

public interface IUserService
{
    Session? GetUserSession();
    string? CurrentUserId();
    Task<UserAdditionError> SignUpUser(string email, string password, string username);
    Task<UserSignInError> SignInUser(string email, string password);
    Task<UserSignoutError> SignOutUser();
}