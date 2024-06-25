namespace Football.Domain.Entities.Football
{
    using System.Collections.Generic;

    public partial class MatchFootball
    {
        public MatchFootball()
        {
            HousePlayers = new HashSet<PlayerMatchHouseFootball>();
            AwayPlayers = new HashSet<PlayerMatchAwayFootball>();
        }

        public int Id { get; set; }

        public int HouseManagerId { get; set; }
        public ManagerFootball HouseManager { get; set; }
        public int AwayManagerId { get; set; }
        public ManagerFootball AwayManager { get; set; }

        public virtual ICollection<PlayerMatchHouseFootball> HousePlayers { get; set; }
        public virtual ICollection<PlayerMatchAwayFootball> AwayPlayers { get; set; }

        public int RefereeId { get; set; }
        public RefereeFootball Referee { get; set; }
    }
}
