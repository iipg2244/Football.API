namespace Football.Domain.Entities.Exemples
{
    using System.Collections.Generic;
    using global::Football.Domain.Entities.Football;

    public class MatchFootballExemple
    {
        public MatchFootballExemple()
        {
            HousePlayers = new HashSet<PlayerMatchHouseFootball>();
            AwayPlayers = new HashSet<PlayerMatchAwayFootball>();
        }

        public ManagerFootballExemple HouseManager { get; set; }
        public ManagerFootballExemple AwayManager { get; set; }

        public virtual ICollection<PlayerMatchHouseFootball> HousePlayers { get; set; }
        public virtual ICollection<PlayerMatchAwayFootball> AwayPlayers { get; set; }

        public RefereeFootballExemple Referee { get; set; }
    }
}
