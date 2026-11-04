namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("sport")]
public class Sport : ObservableBaseModel
{
    readonly Guid _sport_id = Guid.Empty;
    string _name = "";

    [PrimaryKey("sport_id"), Column("sport_id")]
    public Guid SportId
    {
        get { return _sport_id; }
    }

    [Column("name")]
    public string Name
    {
        get { return _name; }
        set { SetProperty(ref _name, value); }
    }


    public Sport() { }

    public Sport(Guid sportId,
                string name = "")
    {
        _sport_id = sportId;
        _name = name;
    }

    public override string ToString()
    {
        return string.Format("Id: {0}, Name: {1}", SportId, Name);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Sport other = (Sport)obj;
        return SportId == other.SportId;
    }

    public override int GetHashCode()
    {
        return SportId.GetHashCode();
    }
}
