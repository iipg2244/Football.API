namespace Football.Domain.Contracts
{
    using Football.Domain.Entities.Football;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMatchRepository
    {
        Task<IEnumerable<MatchFootball>> GetAsync();
        Task<MatchFootball> GetByIdAsync(int id);
        Task<MatchFootball> CreateAsync(MatchFootball match);
        Task<MatchFootball> UpdateAsync(int id, MatchFootball match);
        Task<bool> DeleteAsync(int id);
    }
}
