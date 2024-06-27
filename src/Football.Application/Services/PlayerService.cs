namespace Football.Application.Services
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Football.Domain.Contracts;
    using Football.Domain.Entities.Football;
    using Football.Domain.Extensions;

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository) =>  _playerRepository = playerRepository;

        //
        // Summary:
        //     Returns the entire list of players ordered by name.
        //
        // Returns:
        //     A List<Player>.
        public async Task<IEnumerable<PlayerFootball>> GetPlayersAsync() => await _playerRepository.GetAsync();

        //
        // Summary:
        //     Returns the player by id.
        //
        // Parameters:
        //     id:
        //     Player Identifier Code.
        //
        // Returns:
        //     A Player or null if not found or there is an error.
        public async Task<PlayerFootball> GetPlayerByIdAsync(int id) => await _playerRepository.GetByIdAsync(id);

        //
        // Summary:
        //     Add sent player.
        //
        // Parameters:
        //     player:
        //     Player object.
        //
        // Returns:
        //     The added player or null if there is an error.
        public async Task<PlayerFootball> CreatePlayerAsync(PlayerFootball player) => await _playerRepository.CreateAsync(player);

        //
        // Summary:
        //     Update the player by id.
        //
        // Parameters:
        //     id:
        //     Player Identifier Code.
        //
        //     player:
        //     Player object.
        //
        // Returns:
        //     The updated player or null if there is an error.
        public async Task<PlayerFootball> UpdatePlayerAsync(int id, PlayerFootball player) => await _playerRepository.UpdateAsync(id, player);

        //
        // Summary:
        //     Delete the player by id.
        //
        // Parameters:
        //     id:
        //     Player Identifier Code.
        //
        // Returns:
        //     true is all ok or false if not found or there is an error.
        public async Task<bool> DeletePlayerAsync(int id) => await _playerRepository.DeleteAsync(id);

        ~PlayerService()
        {
        }
    }
}
