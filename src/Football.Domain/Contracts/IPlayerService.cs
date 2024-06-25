namespace Football.Domain.Contracts
{
    using Football.Domain.Entities.Football;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlayerService
    {
        Task<IEnumerable<PlayerFootball>> GetPlayersAsync();
        Task<PlayerFootball> GetPlayerByIdAsync(int id);
        Task<PlayerFootball> CreatePlayerAsync(PlayerFootball player);
        Task<PlayerFootball> UpdatePlayerAsync(int id, PlayerFootball player);
        Task<bool> DeletePlayerAsync(int id);
    }
}
