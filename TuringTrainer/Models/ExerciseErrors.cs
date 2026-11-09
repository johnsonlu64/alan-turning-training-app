namespace TuringTrainer.Models;

public enum ExerciseAdditionError
{
    ExerciseAlreadyExists,
    DuplicateExerciseId,
    InvalidId,
    InvalidName,
    DBAdditionError,
    NoError
}

public enum ExerciseDeletionError
{
    ExerciseNotFound,
    DBDeletionError,
    NoError
}

public enum ExerciseEditError
{
    ExerciseNotFound,
    InvalidFieldError,
    DBEditError,
    NoError
}