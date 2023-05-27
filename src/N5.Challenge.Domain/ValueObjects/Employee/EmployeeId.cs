namespace N5.Challenge.Domain.ValueObjects.Employee
{
    public record EmployeeId
    {
        public Guid Value { get; init; }

        internal EmployeeId(Guid value)
        {
            Value = value;
        }

        public static EmployeeId Create(Guid value)
        {
            Validate(value);
            return new EmployeeId(value);
        }

        private static void Validate(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Id should not be empty", nameof(value));
            }
        }

        public static implicit operator Guid(EmployeeId value)
        {
            return value.Value;
        }
    }
}
