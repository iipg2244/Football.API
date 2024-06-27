namespace Football.Domain.Entities.Exemples
{
    public abstract class PlayerMatchFootballExemple
    {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public PlayerFootballExemple Player { get; set; }
    }
}
