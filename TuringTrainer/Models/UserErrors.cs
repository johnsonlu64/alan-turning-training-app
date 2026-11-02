namespace MovieExtravaganza2.Model;

public enum MovieAdditionError
{
    InvalidIdLength,
    InvalidTitleLength,
    InvalidNumStars,
    InvalidYear,
    DuplicateMovieId,
    DBAdditionError,
    NoError
}

public enum MovieDeletionError
{
    MovieNotFound,
    DBDeletionError,
    NoError
}

public enum MovieEditError
{
    MovieNotFound,
    InvalidFieldError,
    DBEditError,
    NoError
}
