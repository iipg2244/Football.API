namespace Football.Domain.Entities.Football
{
    public partial class Player
    {
#pragma warning disable S3400 // Methods should not return constants
        public bool ShouldSerializeMatchesAway() => false;
#pragma warning restore S3400 // Methods should not return constants

#pragma warning disable S3400 // Methods should not return constants
        public bool ShouldSerializeMatchesHouse() => false;
#pragma warning restore S3400 // Methods should not return constants
    }
}
