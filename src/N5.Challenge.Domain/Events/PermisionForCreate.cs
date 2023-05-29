using N5.Challenge.Common;
using N5.Challenge.Domain.ValueObjects.PermissionType;

namespace N5.Challenge.Domain.Events
{
    public record PermisionForCreate(Guid EmployeeId, List<PermissionTypeId> PermissionTypesId) : IDomainEvent { }
}
