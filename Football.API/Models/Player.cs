using System.Collections.Generic;

namespace Football.API.Models
{
    public partial class Player
    {
        public Player()
        {
            MatchesAway = new HashSet<PlayerMatchAway>();
            MatchesHouse = new HashSet<PlayerMatchHouse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int YellowCard { get; set; }
        public int RedCard { get; set; }
        public int MinutesPlayed { get; set; }

        public virtual ICollection<PlayerMatchAway> MatchesAway { get; set; }
        public virtual ICollection<PlayerMatchHouse> MatchesHouse { get; set; }
    }
}
