using System.Collections.Generic;

namespace DDBB
{
    public partial class Player : Person
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
