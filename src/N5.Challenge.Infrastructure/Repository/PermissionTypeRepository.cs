using Microsoft.EntityFrameworkCore;
using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Repositories;
using N5.Challenge.Domain.ValueObjects.PermissionType;

namespace N5.Challenge.Infrastructure.Repository
{
    public class PermissionTypeRepository : IPermissionTypeRepository
    {
        private readonly N5DBContext _dbContext;

        public PermissionTypeRepository(N5DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<PermissionType>> GetAllAsync()
            => await _dbContext.PermissionTypes.ToListAsync();

        public async Task<PermissionType> GetByKeyAsync(PermissionTypeId key)
            => await _dbContext.PermissionTypes.FindAsync(key.Value);

        public async Task PostAsync(PermissionType entity)
        {
            _dbContext.PermissionTypes.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task PutAsync(PermissionType entity)
        {
            var entityToUpdate = await _dbContext.PermissionTypes.FindAsync(entity.Id);
            if (entityToUpdate is null)
                throw new ArgumentNullException($"PermissionType {entity.Id} not exist");
            _dbContext.PermissionTypes.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
