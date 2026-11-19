namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("class_students")]
public class ClassStudent : ObservableBaseModel
{
	Guid _class_id = Guid.Empty;
	Guid _student_id = Guid.Empty;

	[PrimaryKey("class_id", false), Column("class_id")]
	public Guid ClassId
	{
		get { return _class_id; }
		set { SetProperty(ref _class_id, value); }
	}

	[PrimaryKey("student_id", false), Column("student_id")]
	public Guid StudentId
	{
		get { return _student_id; }
		set { SetProperty(ref _student_id, value); }
	}

	public ClassStudent() { }

	public ClassStudent(Guid classId, Guid studentId)
	{
		_class_id = classId;
		_student_id = studentId;
	}
}
