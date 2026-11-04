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

  public async Task<UserAdditionError> SignUpUser(string email, string password)
  {
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
      return UserAdditionError.InvalidId;
    }
    
    if (password.Length < 6)
    {
      return UserAdditionError.PasswordTooShort;
    }
    
    return await _userRepository.SignUpUser(email, password);
  }
  public async Task<UserSignInError> SignInUser(string email, string password)
  {
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
      return UserSignInError.InvalidEmailOrPassword;
    }
    
    bool isSuccessful = await _userRepository.SignInUser(email, password);

    if (isSuccessful)
    {
      return UserSignInError.NoError;
    } else
    {
      return UserSignInError.InvalidEmailOrPassword;
    }
  }



}