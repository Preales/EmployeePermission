using N5.Challenge.Domain.Repositories;
using N5.Challenge.Domain.ValueObjects.Permission;

namespace N5.Challenge.Domain.Entities
{
    public class Permission
    {
        public Guid Id { get; init; }
        public Guid EmployeeId { get; private set; }
        public Guid PermissionTypeId { get; private set; }
        public PermissionType PermissionType { get; set; }
        public Employee Employee { get; set; }

        public Permission(PermissionId id)
        {
            Id = id;
        }

        public Permission() { }

        public async Task SetEmployeeId(Employee employee, IEmployeeRepository repositoryEmployee)
        {
            await ValidateEmployee(employee, repositoryEmployee);
            EmployeeId = employee.Id;
        }

        private async Task ValidateEmployee(Employee employee, IEmployeeRepository repositoryEmployee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee), "employee cannot be null");
            var type = await repositoryEmployee.GetByKeyAsync(ValueObjects.Employee.EmployeeId.Create(employee.Id));
            if (type is null)
                throw new ArgumentException("Employee specified not exist");
        }

        public async Task SetPermissionTypeId(PermissionType permissionType, IPermissionTypeRepository repositoryPermissionType)
        {
            await ValidatePermissionType(permissionType, repositoryPermissionType);
            PermissionTypeId = permissionType.Id;
        }

        private async Task ValidatePermissionType(PermissionType permissionType, IPermissionTypeRepository repositoryPermissionType)
        {
            if (permissionType is null)
                throw new ArgumentNullException(nameof(permissionType), "permissionType cannot be null");
            var type = await repositoryPermissionType.GetByKeyAsync(ValueObjects.PermissionType.PermissionTypeId.Create(permissionType.Id));
            if (type is null)
                throw new ArgumentException("Type specified is not valid");
        }
    }
}
