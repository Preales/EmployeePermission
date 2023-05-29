using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Repositories.Base;
using N5.Challenge.Domain.ValueObjects.Employee;

namespace N5.Challenge.Domain.Repositories
{
    public interface IEmployeeRepository : IRead<Employee, EmployeeId>, ICreate<Employee>, IUpdate<Employee>
    {
    }
}