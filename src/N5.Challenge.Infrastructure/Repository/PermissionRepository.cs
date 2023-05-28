using Microsoft.EntityFrameworkCore;
using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Repositories;
using N5.Challenge.Domain.ValueObjects.Permission;

namespace N5.Challenge.Infrastructure.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly N5DBContext _dbContext;

        public PermissionRepository(N5DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Permission>> GetAllAsync()
            => await _dbContext.Permissions.ToListAsync();

        public async Task<Permission> GetByKeyAsync(PermissionId key)
            => await _dbContext.Permissions.FindAsync(key.Value);

        public async Task AddAsync(Permission entity)
        {
            _dbContext.Permissions.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Permission entity)
        {
            var entityToUpdate = await _dbContext.Permissions.FindAsync(entity.Id);
            if (entityToUpdate is null)
                throw new ArgumentNullException($"Permission {entity.Id} not exist");
            _dbContext.Permissions.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
