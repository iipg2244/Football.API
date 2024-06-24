namespace Football.Domain.Entities.Football
{
    using System.Collections.Generic;

    public partial class Referee : Person, IMinutes
    {
        public Referee() => Matches = new HashSet<Match>();

        public int MinutesPlayed { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}
