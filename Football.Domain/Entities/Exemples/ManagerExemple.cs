namespace Football.Domain.Entities.Exemples
{
    using global::Football.Domain.Entities.Football;

    public class ManagerExemple : Person, ICards
    {
        public ManagerExemple()
        {
        }

        public int YellowCard { get; set; } = 0;
        public int RedCard { get; set; } = 0;

    }
}
