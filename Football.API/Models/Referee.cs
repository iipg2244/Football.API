using System.Collections.Generic;

namespace Football.API.Models
{
    public class Referee
    {
        public Referee()
        {
            Matches = new HashSet<Match>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int MinutesPlayed { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}
