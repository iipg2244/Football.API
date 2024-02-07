namespace Football.Domain.Entities.Exemples
{
    using Football.Infrastructure;

    public class RefereeExemple : Person, IMinutes
    {
        public RefereeExemple()
        {
        }

        public int MinutesPlayed { get; set; }

    }
}
