using N5.Challenge.Domain.ValueObjects.PermissionType;

namespace N5.Challenge.Domain.Test
{
    public class PermissionTypeTest
    {
        [Fact]
        public void PermissionTypeIdCanBeSetToAValidId()
        {
            var newId = Guid.NewGuid();
            var employee = new PermissionType(PermissionTypeId.Create(newId));
            Assert.Equal(newId, employee.Id);
        }

        [Fact]
        public void PermissionTypeIdCannotBeSetToAnEmptyId()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _ = new Entities.PermissionType(PermissionTypeId.Create(Guid.Empty));
            });
        }

        [Fact]
        public void PermissionTypeNameCanBeSetToAValidName()
        {
            var employee = new Entities.PermissionType(PermissionTypeId.Create(Guid.NewGuid()));
            employee.SetName(PermissionTypeName.Create("Jhon"));
            Assert.Equal("Jhon", employee.Name);
        }

        [Fact]
        public void PermissionTypeNameCannotBeSetToAnEmptyName()
        {
            var employee = new Entities.PermissionType(PermissionTypeId.Create(Guid.NewGuid()));
            Assert.Throws<ArgumentException>(() =>
            {
                employee.SetName(PermissionTypeName.Create(string.Empty));
            });
        }

        [Fact]
        public void PermissionTypeNameCannotBeSetToAnInvalidName()
        {
            var employee = new Entities.PermissionType(PermissionTypeId.Create(Guid.NewGuid()));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                employee.SetName(PermissionTypeName.Create(Guid.NewGuid().ToString()));
            });
        }
    }
}