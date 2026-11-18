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
    Task<UserAvatarError> UploadAvatar(Guid userId, FileResult photo);
    Task<UserAvatarError> UpdateAvatar(Guid userId, string? avatarUrl);
    Task<UserEditError> UpdateUser(User user, string firstName, string middleInitial, string lastName, bool isCoach);
}