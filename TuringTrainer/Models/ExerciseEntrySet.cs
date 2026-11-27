namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("exerciseentryset")]
public class ExerciseEntrySet : ObservableBaseModel
{
    Guid _exercise_entry_set_id = Guid.Empty;
    Guid _exercise_entry_id = Guid.Empty;
    int _reps;
    double? _weight;
    DateTime _end_time;

    [PrimaryKey("exercise_entry_set_id"), Column("exercise_entry_set_id")]
    public Guid ExerciseEntrySetId
    {
        get { return _exercise_entry_set_id; }
        set { SetProperty(ref _exercise_entry_set_id, value); }
    }

    [Column("exercise_entry_id")]
    public Guid ExerciseEntryId
    {
        get { return _exercise_entry_id; }
        set { SetProperty(ref _exercise_entry_id, value); }
    }

    [Column("reps")]
    public int Reps
    {
        get { return _reps; }
        set { SetProperty(ref _reps, value); }
    }

    [Column("weight")]
    public double? Weight
    {
        get { return _weight; }
        set { SetProperty(ref _weight, value); }
    }

    [Column("end_time")]
    public DateTime EndTime
    {
        get { return _end_time; }
        set { SetProperty(ref _end_time, value); }
    }
}