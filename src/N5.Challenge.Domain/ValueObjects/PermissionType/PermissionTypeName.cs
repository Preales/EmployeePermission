namespace N5.Challenge.Domain.ValueObjects.PermissionType
{
    public record PermissionTypeName
    {
        public string Value { get; init; }
        internal PermissionTypeName(string value)
        {
            Value = value;
        }

        public static PermissionTypeName Create(string value)
        {
            Validate(value);
            return new PermissionTypeName(value);
        }

        public static implicit operator string(PermissionTypeName name)
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