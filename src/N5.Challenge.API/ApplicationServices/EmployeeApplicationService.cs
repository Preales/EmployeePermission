using N5.Challenge.API.Commands.Employee;
using N5.Challenge.API.Queries;
using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Repositories;
using N5.Challenge.Domain.ValueObjects.Employee;

namespace N5.Challenge.API.ApplicationServices
{
    public class EmployeeApplicationService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeApplicationService(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task HandleCommandAsync(CreateEmployeeCommand command)
        {
            var employee = new Employee(EmployeeId.Create(command.Id));
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

        public async Task<IList<Employee>> HandleQueryAsync(GetEmployeesQuery query)
            => await _employeeRepository.GetAllAsync();

        public async Task<Employee> HandleQueryAsync(GetEmployeeByIdQuery query)
            => await _employeeRepository.GetByKeyAsync(EmployeeId.Create(query.id));

    }
}