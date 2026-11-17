namespace TuringTrainer.Models;

using System;
using Supabase.Postgrest.Attributes;

[Table("organizations")]
public class Organization : ObservableBaseModel
{
    Guid _organization_id = Guid.Empty;
    string _name = "";
    readonly DateTimeOffset _created_at;

    [PrimaryKey("organization_id"), Column("organization_id")]
    public Guid OrganizationId
    {
        get { return _organization_id; }
        private set { _organization_id = value; }
    }

    [Column("name")]
    public string Name
    {
        get { return _name; }
        set { SetProperty(ref _name, value); }
    }

    [Column("created_at")]
    public DateTimeOffset CreatedAt
    {
        get { return _created_at; }
    }

    public Organization() { }

    public Organization(Guid organizationId, string name)
    {
        _organization_id = organizationId;
        _name = name;
    }

    public override string ToString()
    {
        return string.Format("Id: {0}, Name: {1}, CreatedAt: {2}", OrganizationId, Name, CreatedAt);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        Organization other = (Organization)obj;
        return OrganizationId == other.OrganizationId;
    }

    public override int GetHashCode()
    {
        return OrganizationId.GetHashCode();
    }
}
