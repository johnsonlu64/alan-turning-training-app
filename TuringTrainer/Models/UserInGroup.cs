namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("user_in_group")]
public class UserInGroup : ObservableBaseModel
{
	readonly Guid _user_id = Guid.Empty;
	readonly Guid _group_id = Guid.Empty;

	[PrimaryKey("user_id", false), Column("user_id")]
	public Guid UserId
	{
		get { return _user_id; }
	}

	[PrimaryKey("group_id", false), Column("group_id")]
	public Guid GroupId
	{
		get { return _group_id; }
	}

	public UserInGroup() { }

	public UserInGroup(Guid userId, Guid groupId)
	{
		_user_id = userId;
		_group_id = groupId;
	}
}