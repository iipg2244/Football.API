namespace Football.Domain.Contracts.Refit
{
    using System;

    public enum Version { v1 = 1, v2 = 2 }

    public class ApiVersion
    {
        public Contracts.Refit.Version Value { get; }

        private ApiVersion(int value)
        {
            Value = (Contracts.Refit.Version)value;
        }

        public static readonly ApiVersion v1 = new ApiVersion(1);
        public static readonly ApiVersion v2 = new ApiVersion(2);

        public static explicit operator ApiVersion(int value)
        {
            return value switch
            {
                1 => v1,
                2 => v2,
                _ => throw new ArgumentException("Invalid version value", nameof(value))
            };
        }

        public static implicit operator int(ApiVersion version)
        {
            return (int)version.Value;
        }
    }
}
