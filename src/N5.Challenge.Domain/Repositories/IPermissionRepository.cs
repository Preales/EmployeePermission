using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Repositories.Base;
using N5.Challenge.Domain.ValueObjects.Permission;

namespace N5.Challenge.Domain.Repositories
{
    public interface IPermissionRepository : IRead<Permission, PermissionId>, ICreate<Permission>, IUpdate<Permission>
    {
    }
}
