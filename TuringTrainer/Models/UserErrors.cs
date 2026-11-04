namespace TuringTrainer.Models;

public enum UserSignUpError
{
    MissingEmailOrPassword,
    NoError
}

public enum UserSignInError
{
    InvalidEmailOrPassword,
    DBSigninError,
    NoError
}

public enum UserAdditionError
{
    UserAlreadyExists,
    InvalidId,
    InvalidFirstName,
    InvalidLastName,
    InvalidMiddleInitial,
    DuplicateUserId,
    PasswordTooShort,
    DBAdditionError,
    NoError
}

public enum UserDeletionError
{
    UserNotFound,
    DBDeletionError,
    NoError
}

public enum UserEditError
{
    UserNotFound,
    InvalidFieldError,
    DBEditError,
    NoError
}
