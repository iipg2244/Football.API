namespace Football.Domain.Contracts
{
    using Football.Domain.Entities.Football;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlayerRepository
    {
        Task<IEnumerable<PlayerFootball>> GetAsync();
        Task<PlayerFootball> GetByIdAsync(int id);
        Task<PlayerFootball> CreateAsync(PlayerFootball player);
        Task<PlayerFootball> UpdateAsync(int id, PlayerFootball player);
        Task<bool> DeleteAsync(int id);
    }
}
