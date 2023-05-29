using N5.Challenge.API.Commands.Permission;
using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Events;
using N5.Challenge.Domain.Repositories;
using N5.Challenge.Domain.ValueObjects.Employee;
using N5.Challenge.Domain.ValueObjects.Permission;
using N5.Challenge.Domain.ValueObjects.PermissionType;

namespace N5.Challenge.API.ApplicationServices
{
    public class PermissionApplicationService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPermissionTypeRepository _permissionTypeRepository;


        public PermissionApplicationService(
            IPermissionRepository permissionRepository
            , IEmployeeRepository employeeRepository
            , IPermissionTypeRepository permissionTypeRepository)
        {
            _permissionRepository = permissionRepository;
            _employeeRepository = employeeRepository;
            _permissionTypeRepository = permissionTypeRepository;

            DomainEvents.PermisionForCreate.Register(async parameters =>
            {
                foreach (var permissionTypeId in parameters.PermissionTypesId)
                    await HandleCommandAsync(new CreatePermissionCommand(parameters.EmployeeId, permissionTypeId));
            });
        }

        public async Task HandleCommandAsync(CreatePermissionCommand command)
        {
            var permission = new Permission(PermissionId.Create(Guid.NewGuid()));
            await PermissionSetElement(permission, command.EmployeeId, command.PermissionTypeId);
            await _permissionRepository.AddAsync(permission);
        }

        public async Task HandleCommandAsync(SetPermissionCommand command)
        {
            var permission = new Permission(PermissionId.Create(command.Id));
            await PermissionSetElement(permission, command.EmployeeId, command.PermissionTypeId);
            await _permissionRepository.UpdateAsync(permission);
        }

        private async Task PermissionSetElement(Permission permission, Guid employeeId, Guid permissionTypeId)
        {
            await permission.SetEmployeeId(EmployeeId.Create(employeeId), _employeeRepository);
            await permission.SetPermissionTypeId(PermissionTypeId.Create(permissionTypeId), _permissionTypeRepository);
        }
    }
}