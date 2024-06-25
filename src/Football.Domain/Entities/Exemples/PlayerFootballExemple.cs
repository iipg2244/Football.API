namespace Football.Domain.Entities.Exemples
{
    using global::Football.Domain.Entities.Football;

    public class PlayerFootballExemple : PersonFootballExemple
    {
        public PlayerFootballExemple()
        {
        }

        public int YellowCard { get; set; } = 0;
        public int RedCard { get; set; } = 0;
        public int MinutesPlayed { get; set; } = 0;

    }
}
