namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("prs")]
public class PersonalRecord : ObservableBaseModel
{
    Guid _user_id = Guid.Empty;
    Guid _exercise_id = Guid.Empty;
    double? _weight;
    DateTime? _updated_at;

    [PrimaryKey("user_id", false), Column("user_id")]
    public Guid UserId
    {
        get { return _user_id; }
        set { SetProperty(ref _user_id, value); }
    }

    [PrimaryKey("exercise_id", false), Column("exercise_id")]
    public Guid ExerciseId
    {
        get { return _exercise_id; }
        set { SetProperty(ref _exercise_id, value); }
    }

    [Column("weight")]
    public double? Weight
    {
        get { return _weight; }
        set { SetProperty(ref _weight, value); }
    }

    [Column("updated_at")]
    public DateTime? UpdatedAt
    {
        get { return _updated_at; }
        set { SetProperty(ref _updated_at, value); }
    }

    public PersonalRecord() { }

    public PersonalRecord(Guid userId,
                          Guid exerciseId,
                          double? weight = null)
    {
        _user_id = userId;
        _exercise_id = exerciseId;
        _weight = weight;
        _updated_at = DateTime.UtcNow;
    }

    public override string ToString()
    {
        return string.Format("UserId: {0}, ExerciseId: {1}, Weight: {2}",
                             UserId, ExerciseId, Weight);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        PersonalRecord other = (PersonalRecord)obj;
        return UserId == other.UserId &&
               ExerciseId == other.ExerciseId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, ExerciseId);
    }
}
