namespace Football.Domain.Entities.Football
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json.Serialization;

    [ExcludeFromCodeCoverage]
    public class StatisticFootball
    {
        [JsonIgnore]
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string TeamName { get; set; } = string.Empty;

        public int Total { get; set; } = 0;

        public bool ShouldSerializeId()
        {
            return false;
        }
    }
}


