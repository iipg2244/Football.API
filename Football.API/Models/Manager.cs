//using Newtonsoft.Json;
using System.Collections.Generic;


namespace Football.API.Models
{
    public partial class Manager
    {
        public Manager()
        {
            HouseMatches = new HashSet<Match>();
            AwayMatches = new HashSet<Match>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int YellowCard { get; set; }
        public int RedCard { get; set; }

        //[JsonIgnore]
        public virtual ICollection<Match> HouseMatches { get; set; }
        //[JsonIgnore]
        public virtual ICollection<Match> AwayMatches { get; set; }
    }
}
