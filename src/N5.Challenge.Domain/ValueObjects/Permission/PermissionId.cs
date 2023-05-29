namespace N5.Challenge.Domain.ValueObjects.Permission
{
    public record PermissionId
    {
        public Guid Value { get; init; }

        internal PermissionId(Guid value)
        {
            Value = value;
        }

        public static PermissionId Create(Guid value)
        {
            Validate(value);
            return new PermissionId(value);
        }

        private static void Validate(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Id should not be empty", nameof(value));
            }
        }

        public static implicit operator Guid(PermissionId value)
        {
            return value.Value;
        }
    }
}
