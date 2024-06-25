namespace Football.Domain.Contracts
{
    using Football.Domain.Entities.Football;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMatchService
    {
        Task<IEnumerable<MatchFootball>> GetMatchesAsync();
        Task<MatchFootball> GetMatchByIdAsync(int id);
        Task<MatchFootball> CreateMatchAsync(MatchFootball match);
        Task<MatchFootball> UpdateMatchAsync(int id, MatchFootball match);
        Task<bool> DeleteMatchAsync(int id);
    }
}
