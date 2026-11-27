namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("user_in_group")]
public class UserInGroup : ObservableBaseModel
{
	Guid _user_id = Guid.Empty;
	Guid _group_id = Guid.Empty;

	[PrimaryKey("user_id", false), Column("user_id")]
	public Guid UserId
	{
		get { return _user_id; }
		set { SetProperty(ref _user_id, value); }
	}

	[PrimaryKey("group_id", false), Column("group_id")]
	public Guid GroupId
	{
		get { return _group_id; }
		set { SetProperty(ref _group_id, value); }
	}

	public UserInGroup() { }

	public UserInGroup(Guid userId, Guid groupId)
	{
		_user_id = userId;
		_group_id = groupId;
	}
}