using System.Collections.Generic;

namespace DDBB
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
