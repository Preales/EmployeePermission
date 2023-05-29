using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Repositories.Base;
using N5.Challenge.Domain.ValueObjects.PermissionType;

namespace N5.Challenge.Domain.Repositories
{
    public interface IPermissionTypeRepository : IRead<PermissionType, PermissionTypeId>, ICreate<PermissionType>, IUpdate<PermissionType>
    {
    }
}
