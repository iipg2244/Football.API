namespace Football.Domain.Entities.Exemples
{
    using Football.Infrastructure;

    public class PlayerExemple : Person, ICards, IMinutes
    {
        public PlayerExemple()
        {
        }

        public int YellowCard { get; set; } = 0;
        public int RedCard { get; set; } = 0;
        public int MinutesPlayed { get; set; } = 0;

    }
}
