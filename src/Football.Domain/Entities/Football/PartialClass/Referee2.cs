namespace Football.Domain.Entities.Football
{
    public partial class Referee
    {
#pragma warning disable S3400 // Methods should not return constants
        public bool ShouldSerializeMatches() => false;
#pragma warning restore S3400 // Methods should not return constants
    }
}
