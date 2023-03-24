using System.Collections.Generic;

namespace Football.API.Models
{
    public partial class Player
    {
        public bool ShouldSerializeMatchesAway()
        {
            return false;
        }

        public bool ShouldSerializeMatchesHouse()
        {
            return false;
        }
    }
}
