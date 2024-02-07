namespace Football.Domain.Entities.Exemples
{
    using System.Collections.Generic;
    using Football.Infrastructure;

    public class ManagerExemple : Person, ICards
    {
        public ManagerExemple()
        {
        }

        public int YellowCard { get; set; } = 0;
        public int RedCard { get; set; } = 0;

    }
}
