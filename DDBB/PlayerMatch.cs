using System.Collections.Generic;

namespace DDBB
{
    public abstract class PlayerMatch
    {
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
