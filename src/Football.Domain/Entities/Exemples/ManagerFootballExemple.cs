namespace Football.Domain.Entities.Exemples
{
    public class ManagerFootballExemple : PersonFootballExemple
    {
        public ManagerFootballExemple()
        {
        }

        public int YellowCard { get; set; } = 0;
        public int RedCard { get; set; } = 0;

    }
}
