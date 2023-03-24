namespace Football.API.Models
{
    public partial class Manager
    {
        public bool ShouldSerializeHouseMatches()
        {
            return false;
        }

        public bool ShouldSerializeAwayMatches()
        {
            return false;
        }
    }
}
