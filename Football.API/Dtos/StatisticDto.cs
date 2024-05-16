namespace Football.API.Dtos
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class StatisticDto
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string TeamName { get; set; } = string.Empty;

        public int Total { get; set; } = 0;

        public StatisticDto()
        {
        }
    }
}


