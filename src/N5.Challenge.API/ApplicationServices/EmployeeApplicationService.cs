using N5.Challenge.API.Commands.Employee;
using N5.Challenge.API.Queries;
using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Repositories;
using N5.Challenge.Domain.ValueObjects.Employee;
using N5.Challenge.Domain.ValueObjects.PermissionType;

namespace N5.Challenge.API.ApplicationServices
{
    public class EmployeeApplicationService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public EmployeeApplicationService(
            IEmployeeRepository employeeRepository
            , IPermissionTypeRepository permissionTypeRepository)
        {
            _employeeRepository = employeeRepository;
            _permissionTypeRepository = permissionTypeRepository;
        }

        public async Task HandleCommandAsync(CreateEmployeeCommand command)
        {
            var employee = new Employee(EmployeeId.Create(Guid.NewGuid()));
            employee.SetName(EmployeeName.Create(command.Name));
            employee.SetLastName(EmployeeLastName.Create(command.LastName));
            await _employeeRepository.AddAsync(employee);
        }

        public async Task HandleCommandAsync(SetEmployeeCommand command)
        {
            var employee = new Employee(EmployeeId.Create(command.Id));
            employee.SetName(EmployeeName.Create(command.Name));
            employee.SetLastName(EmployeeLastName.Create(command.LastName));
            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task HandleCommandAsync(CreateEmployeeWithPermission command)
        {
            foreach (var permissionTypeId in command.PermissionTypes)
                await ValidatePermissionTypes(PermissionTypeId.Create(permissionTypeId));

            var employee = new Employee(EmployeeId.Create(command.Id));
            employee.SetName(EmployeeName.Create(command.Name));
            employee.SetLastName(EmployeeLastName.Create(command.LastName));
            await _employeeRepository.AddAsync(employee);
            employee.AddPermision(command.PermissionTypes);
        }

        public async Task<IList<Employee>> HandleQueryAsync(GetEmployeesQuery query)
            => await _employeeRepository.GetAllAsync();

        public async Task<Employee> HandleQueryAsync(GetEmployeeByIdQuery query)
            => await _employeeRepository.GetByKeyAsync(EmployeeId.Create(query.id));

        private async Task ValidatePermissionTypes(PermissionTypeId permissionTypeId)
        {
            if (permissionTypeId == Guid.Empty)
                throw new ArgumentNullException(nameof(permissionTypeId), "permissionTypeId cannot be empty");
            var type = await _permissionTypeRepository.GetByKeyAsync(permissionTypeId);
            if (type is null)
                throw new ArgumentException($"PermissionTypeId {permissionTypeId.Value} is not valid");
        }
    }
}