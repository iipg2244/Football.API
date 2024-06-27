namespace Football.Domain.Entities.Exemples
{
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
