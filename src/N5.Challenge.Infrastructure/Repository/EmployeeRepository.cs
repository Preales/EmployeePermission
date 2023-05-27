using Microsoft.EntityFrameworkCore;
using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Repositories;
using N5.Challenge.Domain.ValueObjects.Employee;

namespace N5.Challenge.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly N5DBContext _dbContext;

        public EmployeeRepository(N5DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Employee>> GetAllAsync()
            => await _dbContext.Employees.ToListAsync();

        public async Task<Employee> GetByKeyAsync(EmployeeId key)
            => await _dbContext.Employees.FindAsync(key.Value);

        public async Task PostAsync(Employee entity)
        {
            _dbContext.Employees.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task PutAsync(Employee entity)
        {
            var entityToUpdate = await _dbContext.Employees.FindAsync(entity.Id);
            if (entityToUpdate is null)
                throw new ArgumentNullException($"Employee {entity.Id} not exist");
            _dbContext.Employees.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
