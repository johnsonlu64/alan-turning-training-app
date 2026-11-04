namespace TuringTrainer.Models;

public enum SportAdditionError
{
    SportAlreadyExists,
    DuplicateSportId,
    InvalidId,
    InvalidName,
    DBAdditionError,
    NoError
}

public enum SportDeletionError
{
    SportNotFound,
    DBDeletionError,
    NoError
}

public enum SportEditError
{
    SportNotFound,
    InvalidFieldError,
    DBEditError,
    NoError
}
