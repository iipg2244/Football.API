namespace Football.Domain.Interfaces
{
    using System.Threading.Tasks;

    public interface IStatisticsService
    {
        Task<object> GetYellowCardsAsync();
        Task<object> GetRedCardsAsync();
        Task<object> GetMinutesPlayedAsync();
    }
}
