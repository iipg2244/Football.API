namespace Football.Infrastructure
{
    using System.Collections.Generic;

    public partial class Match
    {
        public Match()
        {
            HousePlayers = new HashSet<PlayerMatchHouse>();
            AwayPlayers = new HashSet<PlayerMatchAway>();
        }

        public int Id { get; set; }

        public int HouseManagerId { get; set; }
        public Manager HouseManager { get; set; }
        public int AwayManagerId { get; set; }
        public Manager AwayManager { get; set; }

        public virtual ICollection<PlayerMatchHouse> HousePlayers { get; set; }
        public virtual ICollection<PlayerMatchAway> AwayPlayers { get; set; }

        public int RefereeId { get; set; }
        public Referee Referee { get; set; }
    }
}
