namespace Football.Infrastructure
{
    using System.Collections.Generic;

    public partial class Player : Person, ICards, IMinutes
    {
        public Player()
        {
            MatchesAway = new HashSet<PlayerMatchAway>();
            MatchesHouse = new HashSet<PlayerMatchHouse>();
        }

        public int YellowCard { get; set; }
        public int RedCard { get; set; }
        public int MinutesPlayed { get; set; }

        public virtual ICollection<PlayerMatchAway> MatchesAway { get; set; }
        public virtual ICollection<PlayerMatchHouse> MatchesHouse { get; set; }
    }
}
