namespace Football.Domain.Entities.Exemples
{
    using global::Football.Domain.Entities.Football;

    public class RefereeExemple : Person, IMinutes
    {
        public RefereeExemple()
        {
        }

        public int MinutesPlayed { get; set; }

    }
}
