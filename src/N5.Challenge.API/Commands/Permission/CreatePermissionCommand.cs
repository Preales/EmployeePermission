namespace N5.Challenge.API.Commands.Permission
{
    public record CreatePermissionCommand(Guid EmployeeId, Guid PermissionTypeId);
}
