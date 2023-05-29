using N5.Challenge.Domain.Repositories;
using N5.Challenge.Domain.ValueObjects.Employee;
using N5.Challenge.Domain.ValueObjects.Permission;
using N5.Challenge.Domain.ValueObjects.PermissionType;

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

        public async Task SetEmployeeId(EmployeeId employeeId, IEmployeeRepository repositoryEmployee)
        {
            await ValidateEmployee(employeeId, repositoryEmployee);
            EmployeeId = employeeId;
        }

        private async Task ValidateEmployee(EmployeeId employeeId, IEmployeeRepository repositoryEmployee)
        {
            if (employeeId == Guid.Empty)
                throw new ArgumentNullException(nameof(employeeId), "employeeId cannot be empty");
            var type = await repositoryEmployee.GetByKeyAsync(employeeId);
            if (type is null)
                throw new ArgumentException("Employee specified not exist");
        }

        public async Task SetPermissionTypeId(PermissionTypeId permissionTypeId, IPermissionTypeRepository repositoryPermissionType)
        {
            await ValidatePermissionType(permissionTypeId, repositoryPermissionType);
            PermissionTypeId = permissionTypeId;
        }

        private async Task ValidatePermissionType(PermissionTypeId permissionTypeId, IPermissionTypeRepository repositoryPermissionType)
        {
            if (permissionTypeId == Guid.Empty)
                throw new ArgumentNullException(nameof(permissionTypeId), "permissionTypeId cannot be empty");
            var type = await repositoryPermissionType.GetByKeyAsync(permissionTypeId);
            if (type is null)
                throw new ArgumentException("Type specified is not valid");
        }
    }
}
