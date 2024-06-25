namespace Football.Domain.Entities.Football
{
    using System.Text.Json.Serialization;

    public partial class ManagerFootball : PersonFootball
    {
        public int YellowCard { get; set; }
        public int RedCard { get; set; }
        [JsonIgnore]
        public int TotalCards => YellowCard + RedCard;

        public bool ShouldSerializeTotalCards()
        {
            return false;
        }
    }
}
