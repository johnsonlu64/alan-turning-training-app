namespace TuringTrainer.Models;

public enum UserSignUpError
{
    MissingEmailOrPassword,
    NoError
}

public enum UserSignInError
{
    InvalidEmailOrPassword,
    NoError
}

public enum UserAdditionError
{
    InvalidId,
    InvalidFirstName,
    InvalidLastName,
    InvalidMiddleInitial,
    DuplicateUserId,
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
