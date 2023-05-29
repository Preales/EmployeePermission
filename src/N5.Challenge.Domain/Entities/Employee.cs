using N5.Challenge.Domain.Events;
using N5.Challenge.Domain.ValueObjects.Employee;
using N5.Challenge.Domain.ValueObjects.PermissionType;

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

        public void AddPermision(List<Guid> PermissionTypesId)
        {
            var permissionType = new List<PermissionTypeId>();
            foreach (var permissionTypeId in PermissionTypesId)
            {
                permissionType.Add(PermissionTypeId.Create(permissionTypeId));
            }
            DomainEvents.PermisionForCreate.Publish(new PermisionForCreate(Id, permissionType));
        }
    }
}