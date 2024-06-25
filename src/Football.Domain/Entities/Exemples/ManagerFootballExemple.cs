namespace Football.Domain.Entities.Exemples
{
    using global::Football.Domain.Entities.Football;

    public class ManagerFootballExemple : PersonFootballExemple
    {
        public ManagerFootballExemple()
        {
        }

        public int YellowCard { get; set; } = 0;
        public int RedCard { get; set; } = 0;

    }
}
