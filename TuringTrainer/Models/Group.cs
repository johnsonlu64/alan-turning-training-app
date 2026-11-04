namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("groups")]
public class Group : ObservableBaseModel
{
	readonly Guid _group_id = Guid.Empty;

	readonly Guid _coach_id = Guid.Empty;

	readonly Guid _sport_id = Guid.Empty;

	[PrimaryKey("group_id"), Column("group_id")]
	public Guid GroupId
	{
		get { return _group_id; }
	}

	[Column("coach_id")]
	public Guid CoachId
	{
		get { return _coach_id; }
	}

	[Column("sport_id")]
	public Guid SportId
	{
		get { return _sport_id; }
	}
	public Group() { }

	public Group(Guid groupId, Guid coachId, Guid sportId)
	{
		_group_id = groupId;
		_coach_id = coachId;
		_sport_id = sportId;
	}
}