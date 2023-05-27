using N5.Challenge.Domain.ValueObjects.PermissionType;

namespace N5.Challenge.Domain.Entities
{
    public class PermissionType
    {
        public Guid Id { get; init; }
        public PermissionTypeName Name { get; private set; }
        public List<Permission> Permissions { get; set; }
        public PermissionType(PermissionTypeId id)
        {
            Id = id;
        }

        public void SetName(PermissionTypeName name)
        {
            Name = name;
        }
    }
}