using System.Collections.Generic;

namespace Football.API.Models
{
    public class Match
    {
        public int Id { get; set; }

        public Manager HouseManager { get; set; }
        public Manager AwayManager { get; set; }

        public ICollection<Player> HousePlayers { get; set; }
        public ICollection<Player> AwayPlayers { get; set; }

        public Referee Referee { get; set; }
    }
}
