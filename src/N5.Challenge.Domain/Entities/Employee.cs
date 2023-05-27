using N5.Challenge.Domain.ValueObjects.Employee;

namespace N5.Challenge.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; init; }
        public EmployeeName Name { get; private set; }
        public EmployeeLastName LastName { get; private set; }
        public List<Permission> Permissions { get; set; }
        public Employee(EmployeeId id)
        {
            Id = id;
        }

        public Employee() { }

        public void SetName(EmployeeName name)
        {
            Name = name;
        }

        public void SetLastName(EmployeeLastName lastName)
        {
            LastName = lastName;
        }
    }
}