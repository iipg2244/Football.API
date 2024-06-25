namespace Football.Domain.Entities.Football
{
    public partial class PlayerFootball : PersonFootball
    {
        public int YellowCard { get; set; }
        public int RedCard { get; set; }
        public int MinutesPlayed { get; set; }
    }
}
