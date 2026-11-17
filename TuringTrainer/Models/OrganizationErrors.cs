namespace TuringTrainer.Models;

public enum OrganizationAdditionError
{
    NoError,
    InvalidOrganizationId,
    InvalidOrganizationName,
    InvalidUserList,
    AlreadyInOrganization,
    DuplicateOrganizationId,
    DBAdditionError
}
