namespace N5.Challenge.Domain.ValueObjects.PermissionType
{
    public record PermissionTypeId
    {
        public Guid Value { get; init; }

        internal PermissionTypeId(Guid value)
        {
            Value = value;
        }

        public static PermissionTypeId Create(Guid value)
        {
            Validate(value);
            return new PermissionTypeId(value);
        }

        public static implicit operator Guid(PermissionTypeId value)
        {
            return value.Value;
        }

        private static void Validate(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Id should not be empty", nameof(value));
            }
        }
    }
}