namespace Football.Application.Services
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Football.Domain.Contracts;
    using Football.Infrastructure;
    using Football.Domain.Entities.Football;

    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository) => _statisticsRepository = statisticsRepository;

        //
        // Summary:
        //     Returns the entire list of of people who have participated in matches and have yellow cards.
        //
        // Returns:
        //     A List<StatisticFootball>.
        public async Task<IEnumerable<StatisticFootball>> GetYellowCardsAsync() => await _statisticsRepository.GetYellowCardsAsync();

        //
        // Summary:
        //     Returns the entire list of of people who have participated in matches and have red cards.
        //
        // Returns:
        //     A List<StatisticFootball>.
        public async Task<IEnumerable<StatisticFootball>> GetRedCardsAsync() => await _statisticsRepository.GetRedCardsAsync();

        //
        // Summary:
        //     Returns the entire list of of people who have participated in matches and have minutes played.
        //
        // Returns:
        //     A List<StatisticFootball>.
        public async Task<IEnumerable<StatisticFootball>> GetMinutesPlayedAsync() => await _statisticsRepository.GetMinutesPlayedAsync();

        ~StatisticsService()
        {
        }
    }
}
