namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("user")]
public class User : ObservableObject
{
    Guid _id = Guid.Empty;
    String _first_name = "";
    String _middle_initial = "";
    String _last_name = "";
    bool _is_coach;
    DateTimeOffset _created_at;

    [PrimaryKey("id"), Column("id")]
    public Guid Id
    {
        get { return _id; }
        set { SetProperty(ref _id, value); }
    }

    [Column("first_name")]
    public String FirstName
    {
        get { return _first_name; }
        set { SetProperty(ref _first_name, value); }
    }

    [Column("middle_initial")]
    public String MiddleInitial
    {
        get { return _middle_initial; }
        set { SetProperty(ref _middle_initial, value); }
    }

    [Column("last_name")]
    public String LastName
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

    public override String ToString()
    {
        return String.Format("Id: {0}, FirstName: {1}, MiddleInitial: {2}, LastName: {3}, IsCoach: {4}, CreatedAt: {5}", Id, FirstName, MiddleInitial, LastName, IsCoach, CreatedAt);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        User other = (User)obj;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
