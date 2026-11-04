using Supabase.Postgrest.Attributes;

namespace TuringTrainer.Models;

[Table("classes")]
public class TrainingClass : ObservableBaseModel
{
    private Guid _classId;
    private Guid? _groupId;
    private Guid _coachId;
    private string _name = string.Empty;

    [PrimaryKey("class_id"), Column("class_id")]
    public Guid ClassId
    {
        get => _classId;
        set => SetProperty(ref _classId, value);
    }

    [Column("group_id")]
    public Guid? GroupId
    {
        get => _groupId;
        set => SetProperty(ref _groupId, value);
    }

    [Column("coach_id")]
    public Guid CoachId
    {
        get => _coachId;
        set => SetProperty(ref _coachId, value);
    }

    [Column("name")]
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
}
