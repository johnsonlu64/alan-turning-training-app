namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("routineentry")]
public class RoutineEntry : ObservableBaseModel
{
    Guid _routine_entry_id = Guid.Empty;
    Guid _user_id = Guid.Empty;
    Guid _routine_id = Guid.Empty;
    DateTime _start_time;
    DateTime _end_time;
    string _status = "completed";

    [PrimaryKey("routine_entry_id"), Column("routine_entry_id")]
    public Guid RoutineEntryId
    {
        get { return _routine_entry_id; }
        set { SetProperty(ref _routine_entry_id, value); }
    }

    [Column("user_id")]
    public Guid UserId
    {
        get { return _user_id; }
        set { SetProperty(ref _user_id, value); }
    }

    [Column("routine_id")]
    public Guid RoutineId
    {
        get { return _routine_id; }
        set { SetProperty(ref _routine_id, value); }
    }

    [Column("start_time")]
    public DateTime StartTime
    {
        get { return _start_time; }
        set { SetProperty(ref _start_time, value); }
    }

    [Column("end_time")]
    public DateTime EndTime
    {
        get { return _end_time; }
        set { SetProperty(ref _end_time, value); }
    }

    [Column("status")]
    public string Status
    {
        get { return _status; }
        set { SetProperty(ref _status, value); }
    }
}