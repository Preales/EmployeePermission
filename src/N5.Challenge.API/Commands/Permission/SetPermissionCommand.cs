namespace N5.Challenge.API.Commands.Permission
{
    public record SetPermissionCommand(Guid Id, Guid EmployeeId, Guid PermissionTypeId);
}
