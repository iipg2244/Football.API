namespace Football.Domain.Contracts
{
    using System.Threading.Tasks;

    public interface IStatisticsService
    {
        Task<object> GetYellowCardsAsync();
        Task<object> GetRedCardsAsync();
        Task<object> GetMinutesPlayedAsync();
    }
}
