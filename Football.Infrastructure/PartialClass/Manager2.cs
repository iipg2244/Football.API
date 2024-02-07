namespace Football.Infrastructure
{
    public partial class Manager
    {
#pragma warning disable S3400 // Methods should not return constants
        public bool ShouldSerializeHouseMatches() => false;
#pragma warning restore S3400 // Methods should not return constants

#pragma warning disable S3400 // Methods should not return constants
        public bool ShouldSerializeAwayMatches() => false;
#pragma warning restore S3400 // Methods should not return constants
    }
}
