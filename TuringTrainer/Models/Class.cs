namespace TuringTrainer.Models;

using Supabase.Postgrest.Attributes;

[Table("classes")]
public class Class : ObservableBaseModel
{
    private readonly Guid _classId = Guid.Empty;
    private readonly Guid _coachId = Guid.Empty;
    private string _name = string.Empty;

    [PrimaryKey("class_id"), Column("class_id")]
    public Guid ClassId => _classId;

    [Column("coach_id")]
    public Guid CoachId => _coachId;

    [Column("name")]
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public Class()
    {
    }

    public Class(Guid classId, Guid coachId, string name)
    {
        _classId = classId;
        _coachId = coachId;
        _name = name;
    }
}
