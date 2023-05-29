namespace N5.Challenge.API.Commands.Employee
{
    public record CreateEmployeeWithPermission(Guid Id, string Name, string LastName, List<Guid> PermissionTypes);
}
