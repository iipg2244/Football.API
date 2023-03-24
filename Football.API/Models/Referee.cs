using System.Collections.Generic;

namespace Football.API.Models
{
    public partial class Referee : Person
    {
        public Referee()
        {
            Matches = new HashSet<Match>();
        }

        public int MinutesPlayed { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}
