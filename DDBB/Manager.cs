//using Newtonsoft.Json;
using System.Collections.Generic;


namespace DDBB
{
    public partial class Manager :Person, ICards
    {
        public Manager()
        {
            HouseMatches = new HashSet<Match>();
            AwayMatches = new HashSet<Match>();
        }

        public int YellowCard { get; set; }
        public int RedCard { get; set; }

        //[JsonIgnore]
        public virtual ICollection<Match> HouseMatches { get; set; }
        //[JsonIgnore]
        public virtual ICollection<Match> AwayMatches { get; set; }
    }
}
