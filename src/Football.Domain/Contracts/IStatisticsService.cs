namespace Football.Domain.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Football.Domain.Entities.Football;

    public interface IStatisticsService
    {
        Task<IEnumerable<StatisticFootball>> GetYellowCardsAsync();
        Task<IEnumerable<StatisticFootball>> GetRedCardsAsync();
        Task<IEnumerable<StatisticFootball>> GetMinutesPlayedAsync();
    }
}
