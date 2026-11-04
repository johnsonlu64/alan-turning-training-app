using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;
public class UserService
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


}