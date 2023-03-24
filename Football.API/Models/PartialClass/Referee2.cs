using System.Collections.Generic;

namespace Football.API.Models
{
    public partial class Referee
    {
        public bool ShouldSerializeMatches()
        {
            return false;
        }
    }
}
