namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("user_in_organization")]
public class UserInOrganization : ObservableBaseModel
{
    Guid _user_id = Guid.Empty;
    Guid _organization_id = Guid.Empty;
    DateTimeOffset _created_at;

    [Column("user_id")]
    public Guid UserId
    {
        get { return _user_id; }
        set { _user_id = value; }
    }

    [Column("organization_id")]
    public Guid OrganizationId
    {
        get { return _organization_id; }
        set { _organization_id = value; }
    }

    [Column("created_at")]
    public DateTimeOffset CreatedAt
    {
        get { return _created_at; }
        set { _created_at = value; }
    }

    public UserInOrganization() { }

    public UserInOrganization(Guid userId, Guid organizationId)
    {
        _user_id = userId;
        _organization_id = organizationId;
    }
}
