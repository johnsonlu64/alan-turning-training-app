namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("routine")]
public class Routine : ObservableBaseModel
{
    readonly Guid _routine_id = Guid.Empty;
    Guid _classId = Guid.Empty;
    string _name = "";

    [PrimaryKey("routine_id"), Column("routine_id")]
    public Guid RoutineId
    {
        get { return _routine_id; }
    }

    [Column("class_id")]
    public Guid ClassId
    {
        get { return _classId; }
        set { SetProperty(ref _classId, value); }
    }

    [Column("name")]
    public string Name
    {
        get { return _name; }
        set { SetProperty(ref _name, value); }
    }

    public Routine() { }

    public Routine(Guid routineId, Guid classId, string name = "")
    {
        _routine_id = routineId;
        _classId = classId;
        _name = name;
    }
}
