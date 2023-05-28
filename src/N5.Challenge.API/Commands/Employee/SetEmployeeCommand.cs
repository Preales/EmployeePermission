namespace N5.Challenge.API.Commands.Employee
{
    public record SetEmployeeCommand(Guid Id, string Name, string LastName);
}