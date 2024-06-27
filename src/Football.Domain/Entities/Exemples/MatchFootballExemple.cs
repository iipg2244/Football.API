namespace Football.Domain.Entities.Exemples
{
    using System.Collections.Generic;

    public class MatchFootballExemple
    {
        public MatchFootballExemple()
        {
            HousePlayers = new HashSet<PlayerMatchHouseFootballExemple>();
            AwayPlayers = new HashSet<PlayerMatchAwayFootballExemple>();
        }

        public int HouseManagerId { get; set; }
        public ManagerFootballExemple HouseManager { get; set; }
        public int AwayManagerId { get; set; }
        public ManagerFootballExemple AwayManager { get; set; }

        public virtual ICollection<PlayerMatchHouseFootballExemple> HousePlayers { get; set; }
        public virtual ICollection<PlayerMatchAwayFootballExemple> AwayPlayers { get; set; }

        public int RefereeId { get; set; }
        public RefereeFootballExemple Referee { get; set; }
    }
}
