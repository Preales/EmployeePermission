namespace N5.Challenge.Domain.Test
{
    public class EmployeeTest
    {
        [Fact]
        public void EmployeeIdCanBeSetToAValidId()
        {
            var newId = Guid.NewGuid();
            var employee = new Employee(EmployeeId.Create(newId));
            Assert.Equal(newId, employee.Id);
        }

        [Fact]
        public void EmployeeIdCannotBeSetToAnEmptyId()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _ = new Entities.Employee(EmployeeId.Create(Guid.Empty));
            });
        }

        [Fact]
        public void EmployeeNameCanBeSetToAValidName()
        {
            var employee = new Entities.Employee(EmployeeId.Create(Guid.NewGuid()));
            employee.SetName(EmployeeName.Create("Jhon"));
            Assert.Equal("Jhon", employee.Name);
        }

        [Fact]
        public void EmployeeNameCannotBeSetToAnEmptyName()
        {
            var employee = new Entities.Employee(EmployeeId.Create(Guid.NewGuid()));
            Assert.Throws<ArgumentException>(() =>
            {
                employee.SetName(EmployeeName.Create(string.Empty));
            });
        }

        [Fact]
        public void EmployeeNameCannotBeSetToAnInvalidName()
        {
            var employee = new Entities.Employee(EmployeeId.Create(Guid.NewGuid()));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                employee.SetName(EmployeeName.Create(Guid.NewGuid().ToString()));
            });
        }

        [Fact]
        public void EmployeeLastNameCanBeSetToAValidLastName()
        {
            var employee = new Entities.Employee(EmployeeId.Create(Guid.NewGuid()));
            employee.SetLastName(EmployeeLastName.Create("Doe"));
            Assert.Equal("Doe", employee.LastName);
        }

        [Fact]
        public void EmployeeLastNameCannotBeSetToAnEmptyLastName()
        {
            var employee = new Entities.Employee(EmployeeId.Create(Guid.NewGuid()));
            Assert.Throws<ArgumentException>(() =>
            {
                employee.SetLastName(EmployeeLastName.Create(string.Empty));
            });
        }

        [Fact]
        public void EmployeeLastNameCannotBeSetToAnInvalidLastName()
        {
            var employee = new Entities.Employee(EmployeeId.Create(Guid.NewGuid()));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                employee.SetLastName(EmployeeLastName.Create(Guid.NewGuid().ToString()));
            });
        }
    }
}