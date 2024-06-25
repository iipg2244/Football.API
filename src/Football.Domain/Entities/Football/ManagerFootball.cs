namespace Football.Domain.Entities.Football
{
    public partial class ManagerFootball : PersonFootball
    {
        public int YellowCard { get; set; }
        public int RedCard { get; set; }
    }
}
