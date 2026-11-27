namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("exerciseentry")]
public class ExerciseEntry : ObservableBaseModel
{
    Guid _exercise_entry_id = Guid.Empty;
    Guid _routine_entry_id = Guid.Empty;
    Guid _exercise_id = Guid.Empty;

    [PrimaryKey("exercise_entry_id"), Column("exercise_entry_id")]
    public Guid ExerciseEntryId
    {
        get { return _exercise_entry_id; }
        set { SetProperty(ref _exercise_entry_id, value); }
    }

    [Column("routine_entry_id")]
    public Guid RoutineEntryId
    {
        get { return _routine_entry_id; }
        set { SetProperty(ref _routine_entry_id, value); }
    }

    [Column("exercise_id")]
    public Guid ExerciseId
    {
        get { return _exercise_id; }
        set { SetProperty(ref _exercise_id, value); }
    }
}
