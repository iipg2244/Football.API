namespace Football.Domain.Entities.Football
{
    public abstract class PlayerMatchFootball
    {
        public int MatchId { get; set; }
        public MatchFootball Match { get; set; }
        public int PlayerId { get; set; }
        public PlayerFootball Player { get; set; }
    }
}
