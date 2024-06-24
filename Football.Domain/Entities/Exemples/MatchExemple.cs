namespace Football.Domain.Entities.Exemples
{
    using System.Collections.Generic;
    using global::Football.Domain.Entities.Football;

    public class MatchExemple
    {
        public MatchExemple()
        {
            HousePlayers = new HashSet<PlayerMatchHouse>();
            AwayPlayers = new HashSet<PlayerMatchAway>();
        }

        public int Id { get; set; }

        public int HouseManagerId { get; set; }
        public ManagerExemple HouseManager { get; set; }
        public int AwayManagerId { get; set; }
        public ManagerExemple AwayManager { get; set; }

        public virtual ICollection<PlayerMatchHouse> HousePlayers { get; set; }
        public virtual ICollection<PlayerMatchAway> AwayPlayers { get; set; }

        public int RefereeId { get; set; }
        public RefereeExemple Referee { get; set; }
    }
}
