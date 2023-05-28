using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.ValueObjects.PermissionType;

namespace N5.Challenge.Infrastructure.Seed
{
    public class N5DBContextSeeder
    {
        private readonly N5DBContext _context;

        public N5DBContextSeeder(N5DBContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            await CheckPermissionType();
        }

        private async Task CheckPermissionType()
        {
            if (!_context.PermissionTypes.Any())
            {
                var permissionTypeRead = CreatePermissionType("Read");
                var permissionTypeCreate = CreatePermissionType("Create");
                var permissionTypeUpdate = CreatePermissionType("Update");
                var permissionTypeDelete = CreatePermissionType("Delete");
                _context.PermissionTypes.Add(permissionTypeRead);
                _context.PermissionTypes.Add(permissionTypeCreate);
                _context.PermissionTypes.Add(permissionTypeUpdate);
                _context.PermissionTypes.Add(permissionTypeDelete);
                await _context.SaveChangesAsync();
            }
        }

        private static PermissionType CreatePermissionType(string name)
        {
            var permissionTypeRead = new PermissionType(PermissionTypeId.Create(Guid.NewGuid()));
            permissionTypeRead.SetName(PermissionTypeName.Create(name));
            return permissionTypeRead;
        }
    }
}
