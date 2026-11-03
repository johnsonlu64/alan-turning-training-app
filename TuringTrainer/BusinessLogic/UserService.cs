using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;
using Xamarin.Google.ErrorProne.Annotations;
public class UserService
{

  private readonly IUserRepository _userRepository;

  private var _user_session;

  public UserService (IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async  GetUserSession()
  {
    return null;
  }

  public async Task<UserSignUpError> SignUpUser(string email, string password)
  {
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
      return UserSignUpError.MissingEmailOrPassword;
    }
    
    bool isSuccessful = await _userRepository.SignUpUser(email, password);

    if (isSuccessful)
    {
      return UserSignUpError.NoError;
    } else
    {
      return UserSignUpError.MissingEmailOrPassword;
    }
  }

  public async Task<UserSignInError> SignUpUser(string email, string password)
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