using System.Collections.Generic;

namespace DDBB
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
