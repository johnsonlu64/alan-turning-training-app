namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("exercise")]
public class Exercise : ObservableBaseModel
{
    readonly Guid _exercise_id = Guid.Empty;
    string _name = "";

    [PrimaryKey("exercise_id"), Column("exercise_id")]
    public Guid ExerciseId
    {
        get { return _exercise_id; }
    }

    [Column("name")]
    public string Name
    {
        get { return _name; }
        set { SetProperty(ref _name, value); }
    }


    public Exercise() { }

    public Exercise(Guid exerciseId,
                    string name = "")
    {
        _exercise_id = exerciseId;
        _name = name;
    }

    public override string ToString()
    {
        return string.Format("Id: {0}, Name: {1}", ExerciseId, Name);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Exercise other = (Exercise)obj;
        return ExerciseId == other.ExerciseId;
    }

    public override int GetHashCode()
    {
        return ExerciseId.GetHashCode();
    }
}