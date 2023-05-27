namespace N5.Challenge.Domain.ValueObjects.Employee
{
    public class EmployeeLastName
    {
        public string Value { get; init; }
        internal EmployeeLastName(string value)
        {
            Value = value;
        }

        public static EmployeeLastName Create(string value)
        {
            validate(value);
            return new EmployeeLastName(value);
        }

        private static void validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("LastName is required", nameof(value));
            if (value.Length > 20)
                throw new ArgumentOutOfRangeException(nameof(value), "LastName must not be longer than 20 characters");
        }
    }
}
