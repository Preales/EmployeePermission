namespace N5.Challenge.Domain.ValueObjects.Employee
{
    public record EmployeeName
    {
        public string Value { get; init; }
        internal EmployeeName(string value)
        {
            Value = value;
        }

        public static EmployeeName Create(string value)
        {
            Validate(value);
            return new EmployeeName(value);
        }

        public static implicit operator string(EmployeeName name)
        {
            return name.Value;
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Name is required", nameof(value));
            if (value.Length > 20)
                throw new ArgumentOutOfRangeException(nameof(value), "Name must not be longer than 20 characters");
        }
    }
}
