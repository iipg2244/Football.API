namespace Football.Domain.Entities.Exemples
{
    using global::Football.Domain.Entities.Football;

    public class RefereeFootballExemple : PersonFootballExemple
    {
        public RefereeFootballExemple()
        {
        }

        public int MinutesPlayed { get; set; } = 0;

    }
}
