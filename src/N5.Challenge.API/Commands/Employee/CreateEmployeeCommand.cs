namespace N5.Challenge.API.Commands.Employee
{
    public record CreateEmployeeCommand(Guid Id, string Name, string LastName);
}