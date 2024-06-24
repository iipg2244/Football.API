namespace Football.Domain.Entities.Football
{
    using System.Collections.Generic;

    public partial class Manager :Person, ICards
    {
        public Manager()
        {
            HouseMatches = new HashSet<Match>();
            AwayMatches = new HashSet<Match>();
        }

        public int YellowCard { get; set; }
        public int RedCard { get; set; }

        public virtual ICollection<Match> HouseMatches { get; set; }
        public virtual ICollection<Match> AwayMatches { get; set; }
    }
}
