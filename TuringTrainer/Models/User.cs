namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("user")]
public class User : ObservableBaseModel
{
    readonly Guid _user_id = Guid.Empty;
    string _first_name = "";
    string _middle_initial = "";
    string _last_name = "";
    bool _is_coach;
    DateTimeOffset _created_at;

    [PrimaryKey("user_id"), Column("user_id")]
    public Guid UserId
    {
        get { return _user_id; }
    }

    [Column("first_name")]
    public string FirstName
    {
        get { return _first_name; }
        set { SetProperty(ref _first_name, value); }
    }

    [Column("middle_initial")]
    public string MiddleInitial
    {
        get { return _middle_initial; }
        set { SetProperty(ref _middle_initial, value); }
    }

    [Column("last_name")]
    public string LastName
    {
        get { return _last_name; }
        set { SetProperty(ref _last_name, value); }
    }

    [Column("is_coach")]
    public bool IsCoach
    {
        get { return _is_coach; }
        set { SetProperty(ref _is_coach, value); }
    }

    [Column("created_at")]
    public DateTimeOffset CreatedAt
    {
        get { return _created_at; }
        set { SetProperty(ref _created_at, value); }
    }

    public User() { }

    public User(Guid userId,
                string firstName = "",
                string middleInitial = "",
                string lastName = "",
                bool isCoach = false)
    {
        _user_id = userId;
        _first_name = firstName;
        _middle_initial = middleInitial;
        _last_name = lastName;
        _is_coach = isCoach;
        _created_at = DateTimeOffset.UtcNow;
    }

    public override string ToString()
    {
        return string.Format("Id: {0}, FirstName: {1}, MiddleInitial: {2}, LastName: {3}, IsCoach: {4}, CreatedAt: {5}", UserId, FirstName, MiddleInitial, LastName, IsCoach, CreatedAt);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        User other = (User)obj;
        return UserId == other.UserId;
    }

    public override int GetHashCode()
    {
        return UserId.GetHashCode();
    }
}
