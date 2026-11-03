using TuringTrainer.Models;
using TuringTrainer.Services.Repositories;
public class UserService
{

  private readonly IUserRepository _userRepository;

  private var _user_session;

  public UserService (IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }



  public async Task<UserAdditionError> SignUpUser(string email, string password)
  {
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
      return UserAdditionError.InvalidId;
    }
    
    _userRepository.SignUpUser(email, password);


    try
    {
      _user_session = await 
    }
  }


}