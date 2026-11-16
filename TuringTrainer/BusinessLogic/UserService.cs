using TuringTrainer.BusinessLogic;
using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;
public class UserService : IUserService
{

  private readonly IUserRepository _userRepository;

  public UserService (IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public Supabase.Gotrue.Session? GetUserSession()
  {
    return _userRepository.GetUserSession();
  }

  public string? CurrentUserId()
  {
    return GetUserSession()?.User?.Id;
  }

  public async Task<User?> CurrentUser()
  {
    string? currentUserId = CurrentUserId();
    if (currentUserId != null)
    {
      return await _userRepository.SelectUser(Guid.Parse(currentUserId));
    }
    else
    {
      return null;
    }
  }

  public async Task<UserAdditionError> SignUpUser(string email, string password, string username)
  {
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
      return UserAdditionError.InvalidId;
    }
    
    if (password.Length < 6)
    {
      return UserAdditionError.PasswordTooShort;
    }
    
    return await _userRepository.SignUpUser(email, password, username);
  }
  public async Task<UserSignInError> SignInUser(string email, string password)
  {
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
      return UserSignInError.InvalidEmailOrPassword;
    }
    
    return await _userRepository.SignInUser(email, password);
  }


  public async Task<UserSignoutError> SignOutUser()
  {
    return await _userRepository.SignOutUser();
  }

  public async Task<UserAvatarError> UploadAvatar(Guid userId, FileResult photo)
  {
      if (userId == Guid.Empty)
          return UserAvatarError.UserNotFound;

      if (Path.GetExtension(photo.FileName).ToLower() != ".png")
          return UserAvatarError.InvalidFileType;

      return await _userRepository.UploadAvatar(userId, photo);
  }

  public async Task<UserAvatarError> UpdateAvatar(Guid userId, string? avatarUrl)
  {
      if (userId == Guid.Empty)
          return UserAvatarError.UserNotFound;

      return await _userRepository.UpdateAvatar(userId, avatarUrl);
  }
}