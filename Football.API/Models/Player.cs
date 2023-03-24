namespace Football.API.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YellowCard { get; set; }
        public int RedCard { get; set; }
        public int MinutesPlayed { get; set; }
    }
}
